using Microsoft.Extensions.Logging;

namespace EquipmentManagement.Application;

public class AppLogEvents
{
    public static readonly EventId Create = new(1000, "Создано");
    public static readonly EventId Read = new(1001, "Считано");
    public static readonly EventId Update = new(1002, "Обновлено");
    public static readonly EventId Delete = new(1003, "Удалено");

    public static readonly EventId Login = new(2001, "Логин");
    public static readonly EventId Register = new(2002, "Регистрация");
}
