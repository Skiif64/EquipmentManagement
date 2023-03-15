using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.Equipments.GetByEmployeeId;

public class GetEquipmentByEmployeeQueryHandler : IQueryHandler<GetEquipmentsByEmployeeIdQuery, IEnumerable<Equipment>>
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IEquipmentStatusRepository _equipmentStatusRepository;

    public GetEquipmentByEmployeeQueryHandler(IEquipmentRepository equipmentRepository,
                                              IEquipmentStatusRepository equipmentStatusRepository)
    {
        _equipmentRepository = equipmentRepository;
        _equipmentStatusRepository = equipmentStatusRepository;
    }
    public async Task<IEnumerable<Equipment>> Handle(GetEquipmentsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var statuses = await _equipmentStatusRepository.GetActualByEmployeeId(request.EmployeeId, cancellationToken);
        var equipments = await _equipmentRepository.GetAllAsync(cancellationToken);
        return equipments.Where(e => statuses.Select(s => s.EquipmentId).Contains(e.Id));
    }
}
