using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Journaling;
public class DbJournal : IJournal
{
    private readonly IApplicationDbContext _context;

    public DbJournal(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task WriteAsync(JournalRecord record, CancellationToken cancellationToken)
    {
        await _context
            .Set<JournalRecord>()
            .AddAsync(record, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
