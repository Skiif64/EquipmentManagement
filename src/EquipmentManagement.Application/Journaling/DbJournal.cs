using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.Extensions.Logging;

namespace EquipmentManagement.Application.Journaling;
public class DbJournal : IJournal
{
    private readonly IApplicationDbContext _context;
    private readonly string _username;

    public DbJournal(string? username, IApplicationDbContext context)
    {
        _context = context;
        _username = username ?? string.Empty;
    }    

    public async Task WriteAsync(EventId eventId,
        string message,
        string? user = null,
        DateTimeOffset? created = null,
        CancellationToken cancellationToken = default)
    {
        var record = new JournalRecord
        {
            Username = user ?? _username,
            EventName = eventId.Name ?? string.Empty,
            DateCreated = created,
            Message = message
        };
        await _context
           .Set<JournalRecord>()
           .AddAsync(record, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
