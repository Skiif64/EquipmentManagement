using System.ComponentModel.DataAnnotations;


namespace EquimentManagement.Contracts;
public class NotDefaultValueAttribute : ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return $"Field {name} is null or default value";
    }

    public override bool IsValid(object? value)
    {
        return value != default;
    }
}
