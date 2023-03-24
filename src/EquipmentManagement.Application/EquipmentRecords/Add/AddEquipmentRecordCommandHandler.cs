using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentRecords.Add;

public class AddEquipmentRecordCommandHandler : ICommandHandler<AddEquipmentRecordCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEquipmentRecordCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(AddEquipmentRecordCommand request, CancellationToken cancellationToken)
    {
        var record = _mapper.Map<EquipmentRecord>(request);
        if (request.EmployeeId is not null)
        {
            var employee = await _context.Set<Employee>().FindAsync(request.EmployeeId);

            record.Employee = employee;
        }
        var equipment = await _context.Set<Equipment>().FindAsync(request.EquipmentId);
        record.Equipment = equipment;
        var status = await _context.Set<Status>().FindAsync(request.StatusId);
        record.Status = status;
        await _context.Set<EquipmentRecord>().AddAsync(record, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return record.Id;
    }
}
