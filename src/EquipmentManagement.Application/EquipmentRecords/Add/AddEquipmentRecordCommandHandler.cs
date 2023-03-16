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
        await _context.Set<EquipmentRecord>().AddAsync(record);
        return record.Id;
    }
}
