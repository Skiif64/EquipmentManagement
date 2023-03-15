using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.GetAll;

public class GetAllEquipmentQueryHandler : IQueryHandler<GetAllEquipmentQuery, IEnumerable<Equipment>>
{
    private readonly IEquipmentRepository _equipmentRepository;

    public GetAllEquipmentQueryHandler(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<IEnumerable<Equipment>> Handle(GetAllEquipmentQuery request, CancellationToken cancellationToken)
    {
        return await _equipmentRepository.GetAllAsync(cancellationToken);
    }
}
