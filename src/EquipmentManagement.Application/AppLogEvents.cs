using Microsoft.Extensions.Logging;

namespace EquipmentManagement.Application;

public class AppLogEvents
{
    public static readonly EventId Create = new(1000, "Create");
    public static readonly EventId Read = new(1001, "Read");
    public static readonly EventId Update = new(1002, "Update");
    public static readonly EventId Delete = new(1003, "Delete");

    public static readonly EventId Login = new(2001, "Login");
    public static readonly EventId Register = new(2002, "Register");
}
