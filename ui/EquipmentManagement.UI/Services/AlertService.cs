using EquipmentManagement.UI.Abstractions;

namespace EquipmentManagement.UI.Services;

public class AlertService : IAlertService
{
    public event EventHandler<AlertParameters> AlertRequested = null!;

    public void Show(AlertType type, string message)
    {
        AlertRequested?.Invoke(this, new AlertParameters(type, message));
    }
}
