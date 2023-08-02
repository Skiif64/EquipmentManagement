using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetEquipmentDtoQueryHandler
    : IQueryHandler<GetEquipmentDtoQuery, PagedList<EquipmentDto>>
{
    private const int Page = 1;
    private const int PageSize = 10;
    private const string Order = "ASC";
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEquipmentDtoQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<EquipmentDto>> Handle(GetEquipmentDtoQuery request,
        CancellationToken cancellationToken)
    {
        var equipments = _context
            .Set<Equipment>()
            .Include(x => x.Images)
            .Include(x => x.Type);

        var records = _context
            .Set<EquipmentRecord>()
            .Where(x => equipments.Contains(x.Equipment))
            .OrderByDescending(x => x.Modified)
            .Include(x => x.Status)
            .Include(x => x.Employee)
            .AsEnumerable()
            .DistinctBy(x => x.EquipmentId)
            .GroupJoin(equipments, inner => inner.EquipmentId, outer => outer.Id, 
            (inner, outer) => new EquipmentDto
            {
                
                Id = outer.First().Id,
                Article = outer.First().Article,
                Author = outer.First().Author,
                CreatedAt = outer.First().CreatedAt,
                CurrentEmployee = inner.Employee,
                CurrentStatus = inner.Status,
                Description = outer.First().Description,
                Images = outer.First().Images,
                SerialNumber = outer.First().SerialNumber,
                Type = outer.First().Type,
                TypeId = outer.First().TypeId,
            })
            //.Zip(equipments, (record, equipment) => 
            .Where(record => SearchSelector(record, request.SearchCriteria))
            .AsQueryable()
            ?? throw new NotFoundException("Records");

        
            

        int page = request.Page ?? Page;
        int pageSize = request.PageSize ?? PageSize;
        return PagedList<EquipmentDto>.Create(records, page, pageSize);
    }

    private bool SearchSelector(EquipmentDto item, string? criteria)
    {        
        var query = criteria ?? string.Empty;
        const StringComparison comparsion = StringComparison.InvariantCultureIgnoreCase;

        if (item.Type.Name.Contains(query, comparsion))
            return true;
        if (item.CurrentStatus?.Title.Contains(query, comparsion) ?? false)
            return true;
        if (item.Article.Contains(query, comparsion))
            return true;
        if (item.SerialNumber.Contains(query, comparsion))
            return true;
        if (item.CurrentEmployee?.Fullname.Contains(query, comparsion) ?? false)
            return true;
        if (item.Description?.Contains(query, comparsion) ?? false)
            return true;

        return false;
    }
}
