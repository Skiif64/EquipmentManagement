using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.Add;

public class AddEquipmentCommandHandler : ICommandHandler<AddEquipmentCommand>
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IMapper _mapper;

    public AddEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IMapper mapper)
    {
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = _mapper.Map<Equipment>(request);
        await _equipmentRepository.CreateAsync(equipment, cancellationToken);
    }
}
