using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace EquipmentManagement.Application.EquipmentRecords.GetActualsByEmployeeID;

public class GetActualsByEmployeeIdQueryHandler : IQueryHandler<GetActualsByEmployeeIdQuery, IEnumerable<EquipmentRecord>?>
{
    private readonly IApplicationDbContext _context;

    public GetActualsByEmployeeIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<EquipmentRecord>?> Handle(GetActualsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var grouppedRecords = _context.Set<EquipmentRecord>()
            .Include(x => x.Employee)
            .Include(x => x.Equipment)
            .Include(x => x.Status)
            .GroupBy(x => x.Equipment.Id)
            ;

        var records = grouppedRecords
            .Select(x => x.MaxBy(t => t.Modified));
        if (!records.Any())
            throw new NotFoundException("EquipmentRecord");

        return Task.FromResult(records?.AsEnumerable());
    }
}
