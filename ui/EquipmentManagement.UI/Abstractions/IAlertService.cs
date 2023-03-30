namespace EquipmentManagement.UI.Abstractions;

public enum AlertType
{
    Primary,
    Secondary,
    Success,
    Danger,
    Warning,
    Info,
    Light,
    Dark
}

public class AlertParameters
{
    public AlertType Type { get; }
    public string Message { get; }
    public AlertParameters(AlertType type, string message)
    {
        Type = type;
        Message = message;
    }
}

public interface IAlertService
{
    event EventHandler<AlertParameters> AlertRequested;
    void Show(AlertType type, string message);
}
