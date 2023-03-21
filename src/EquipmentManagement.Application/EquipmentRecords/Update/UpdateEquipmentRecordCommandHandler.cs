using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.EquipmentRecords.Update;

public class UpdateEquipmentRecordCommandHandler : ICommandHandler<UpdateEquipmentRecordCommand, EquipmentRecord>
{
    private readonly IApplicationDbContext _context;    

    public UpdateEquipmentRecordCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;        
    }

    public async Task<EquipmentRecord> Handle(UpdateEquipmentRecordCommand request, CancellationToken cancellationToken)
    {
        var record = await _context
            .Set<EquipmentRecord>()
            .Include(x => x.Employee)
            .Include(x => x.Equipment)
            .Include(x => x.Status)
            .SingleOrDefaultAsync(r => r.Id == request.EquipmentRecordId, cancellationToken)
            ?? throw new NotFoundException("EquipmentRecord")
            ;
        Employee? employee = null;
        if (request.EmployeeId is not null)
            employee = await _context
                .Set<Employee>()
                .SingleOrDefaultAsync(x => x.Id == request.EmployeeId)
                ?? throw new NotFoundException("Employee");

        record.Employee = employee;
        if (request.StatusId is not null)
        {
            var status = await _context
                .Set<Status>()
                .SingleOrDefaultAsync(x => x.Id == request.StatusId)
                ?? throw new NotFoundException("Status");
            record.Status = status;
        }
        record.Modified= request.Modified;
        _context.Set<EquipmentRecord>().Update(record);
        await _context.SaveChangesAsync(cancellationToken);
        return record;
    }
}
