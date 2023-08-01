using EquimentManagement.Contracts.Responses;

namespace EquipmentManagement.UI.Models;

public class EquipmentViewModel : EquipmentResponse
{
    public string? CurrentStatusTitle { get; init; }
    public string? EmployeeFullname { get; init; }
}
