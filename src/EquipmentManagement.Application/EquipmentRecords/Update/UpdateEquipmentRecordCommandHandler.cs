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
            .SingleOrDefaultAsync(r => r.Id == request.EquipmentRecordId, cancellationToken)
            ?? throw new NotFoundException("EquipmentRecord")
            ;

        record.EmployeeId = request.EmployeeId;
        if (request.StatusId is not null)
            record.StatusId = (Guid)request.StatusId;
        record.Modified= request.Modified;
        _context.Set<EquipmentRecord>().Update(record);
        await _context.SaveChangesAsync(cancellationToken);
        return record;
    }
}
