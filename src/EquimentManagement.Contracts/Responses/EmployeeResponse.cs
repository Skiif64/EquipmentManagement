namespace EquimentManagement.Contracts.Responses;

public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string Firstname { get; init; } = string.Empty;
    public string Lastname { get; init; } = string.Empty;
    public string? Patronymic { get; init; }
}
