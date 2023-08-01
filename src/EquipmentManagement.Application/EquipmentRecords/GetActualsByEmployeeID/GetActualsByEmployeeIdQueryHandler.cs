using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace EquipmentManagement.Application.EquipmentRecords.GetActualsByEmployeeID;

public class GetActualsByEmployeeIdQueryHandler 
    : IQueryHandler<GetActualsByEmployeeIdQuery, IEnumerable<EquipmentRecord>>
{
    private readonly IApplicationDbContext _context;

    public GetActualsByEmployeeIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<EquipmentRecord>> Handle(GetActualsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var records = _context
            .Set<EquipmentRecord>()
            .Include(x => x.Employee)
            .Include(x => x.Equipment)
            .Where(x => x.Employee != null && x.Employee.Id == request.EmployeeId)
            .OrderByDescending(x => x.Modified)            
            .AsEnumerable()
            .DistinctBy(x => x.EquipmentId);        

        return Task.FromResult(records.AsEnumerable());
    }
}
