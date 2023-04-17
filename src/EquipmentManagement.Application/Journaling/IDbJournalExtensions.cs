using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace EquipmentManagement.Application.Abstractions;
public static class IDbJournalExtensions
{
    public static async Task WriteAsync(this IJournal journal, EventId eventId, string message, CancellationToken cancellationToken = default)
    {
        await journal.WriteAsync(eventId, message, null, DateTimeOffset.UtcNow, cancellationToken);
    }

    public static async Task WriteAsync(this IJournal journal, EventId eventId, string message, string? user, CancellationToken cancellationToken = default)
    {
        await journal.WriteAsync(eventId, message, user, DateTimeOffset.UtcNow, cancellationToken);
    }

    public static async Task WriteAsync(this IJournal journal, EventId eventId, string message, ClaimsPrincipal? user, CancellationToken cancellationToken = default)
    {
        var username = user?.Identity?.Name;
        await journal.WriteAsync(eventId, message, username, DateTimeOffset.UtcNow, cancellationToken);
    }
}
