using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Equipments.Get;

public class GetAllEquipmentWithStatusQueryHandler
    : IQueryHandler<GetAllEquipmentWithStatusQuery, PagedList<Equipment>>
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

    public async Task<PagedList<Equipment>> Handle(GetAllEquipmentWithStatusQuery request,
        CancellationToken cancellationToken)
    {
        var entities = _context
            .Set<Equipment>()
            .Include(x => x.Images)
            .Include(x => x.Type);

        int page = request.Page ?? Page;
        int pageSize = request.PageSize ?? PageSize;
        return await PagedList<Equipment>.CreateAsync(entities, page, pageSize);
    }
}
