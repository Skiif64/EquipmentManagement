using MediatR;

namespace EquipmentManagement.Application.Statuses;
public class CreateStatusEvent : INotification
{
    public string Title { get; set; }

    public CreateStatusEvent(string title)
    {
        Title = title;
    }
}
