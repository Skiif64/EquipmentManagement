using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.Extensions.Logging;

namespace EquipmentManagement.Application.Journaling;
public class DbJournal : IJournal
{
    private readonly IApplicationDbContext _context;

    public DbJournal(IApplicationDbContext context)
    {
        _context = context;
    }    

    public async Task WriteAsync(EventId eventId, string message, Guid? user = null, CancellationToken cancellationToken = default)
    {
        var record = new JournalRecord
        {
            ApplicationUserId = user,
            EventName = eventId.Name ?? string.Empty,
            Message = message
        };
        await _context
           .Set<JournalRecord>()
           .AddAsync(record, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
