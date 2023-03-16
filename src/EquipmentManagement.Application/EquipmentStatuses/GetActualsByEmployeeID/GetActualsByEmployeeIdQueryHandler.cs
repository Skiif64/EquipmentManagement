using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.EquipmentStatuses.GetActualByEmployeeID;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;

namespace EquipmentManagement.Application.EquipmentStatuses.GetActualsByEmployeeID;

public class GetActualsByEmployeeIdQueryHandler : IQueryHandler<GetActualsByEmployeeIdQuery, IEnumerable<EquipmentRecord>>
{
    private readonly IEquipmentStatusRepository _equipmentStatusRepository;

    public GetActualsByEmployeeIdQueryHandler(IEquipmentStatusRepository equipmentStatusRepository)
    {
        _equipmentStatusRepository = equipmentStatusRepository;
    }

    public async Task<IEnumerable<EquipmentRecord>> Handle(GetActualsByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var statuses = await _equipmentStatusRepository.GetAllAsync(cancellationToken);
        return statuses
            .Where(s => s.EmployeeId == request.EmployeeId)
            .OrderBy(s => s.Modified)
            .DistinctBy(s => s.EquipmentId);
    }
}
