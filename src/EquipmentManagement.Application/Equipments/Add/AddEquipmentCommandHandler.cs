using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.Add;

public class AddEquipmentCommandHandler : ICommandHandler<AddEquipmentCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEquipmentCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = _mapper.Map<Equipment>(request);
        await _context.Set<Equipment>().AddAsync(equipment,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
