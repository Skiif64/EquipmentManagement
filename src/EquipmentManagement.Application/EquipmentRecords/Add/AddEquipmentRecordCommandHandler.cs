using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Exceptions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.Add;

public class AddEquipmentRecordCommandHandler : ICommandHandler<AddEquipmentRecordCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IJournal _journal;

    public AddEquipmentRecordCommandHandler(IApplicationDbContext context, IMapper mapper, IJournal journal)
    {
        _context = context;
        _mapper = mapper;
        _journal = journal;
    }
    public async Task<Guid> Handle(AddEquipmentRecordCommand request, CancellationToken cancellationToken)
    {
        var record = _mapper.Map<EquipmentRecord>(request);
        if (request.EmployeeId is not null)
        {
            var employee = await _context
                .Set<Employee>()
                .FindAsync(new object[] { request.EmployeeId }, cancellationToken);

            record.Employee = employee;
        }
        var equipment = await _context
            .Set<Equipment>()
            .FindAsync(new object[] { request.EquipmentId }, cancellationToken)
            ?? throw new NotFoundException("Equipment");
        record.Equipment = equipment;
        var status = await _context
            .Set<Status>()
            .FindAsync(new object[] { request.StatusId }, cancellationToken)
            ?? throw new NotFoundException("Status");
        record.Status = status;
        await _context.Set<EquipmentRecord>().AddAsync(record, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _journal.WriteAsync(AppLogEvents.Update,
            $"Изменен статус оборудования: {equipment.Article} {equipment.SerialNumber}. " +
            $"Новый статус: {status.Title}",
            cancellationToken);
        return record.Id;
    }
}
