using Microsoft.Extensions.Logging;

namespace EquipmentManagement.Application;

public class AppLogEvents
{
    public static readonly EventId Create = new(1000, "Create");
    public static readonly EventId Read = new(1000, "Read");
    public static readonly EventId Update = new(1001, "Update");
    public static readonly EventId Delete = new(1000, "Delete");
}
