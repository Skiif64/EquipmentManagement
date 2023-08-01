using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetAllEquipmentWithStatusQueryHandler
    : IQueryHandler<GetAllEquipmentWithStatusQuery, PagedList<EquipmentDto>>
{
    private const int Page = 1;
    private const int PageSize = 10;
    private const string Order = "ASC";
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEquipmentWithStatusQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<EquipmentDto>> Handle(GetAllEquipmentWithStatusQuery request,
        CancellationToken cancellationToken)
    {
        var equipments = _context
            .Set<Equipment>()
            .Include(x => x.Images)
            .Include(x => x.Type);

        var records = _context
            .Set<EquipmentRecord>()
            //.Include(x => x.Equipment)
            .Where(x => equipments.Contains(x.Equipment))
            .OrderByDescending(x => x.Modified)
            .Include(x => x.Status)
            .Include(x => x.Employee)
            .AsEnumerable()
            .DistinctBy(x => x.Id)
            .Zip(equipments, (record, equipment) => new EquipmentDto
            {
                Id = equipment.Id,
                Article = equipment.Article,
                Author = equipment.Author,
                CreatedAt = equipment.CreatedAt,
                CurrentEmployee = record.Employee,
                CurrentStatus = record.Status,
                Description = equipment.Description,
                Images = equipment.Images,
                SerialNumber = equipment.SerialNumber,
                Type = equipment.Type,
                TypeId = equipment.TypeId,
            })            
            ?? throw new NotFoundException("Records");

        int page = request.Page ?? Page;
        int pageSize = request.PageSize ?? PageSize;
        return PagedList<EquipmentDto>.Create(records.AsQueryable(), page, pageSize);
    }
}
