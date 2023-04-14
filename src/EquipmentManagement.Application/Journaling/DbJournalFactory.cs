using EquipmentManagement.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentManagement.Application.Journaling;
public class DbJournalFactory : IJournalFactory, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private DbJournal? _journal;
    private bool _disposed;

    public DbJournalFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJournal Create(string username)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(DbJournalFactory));
        var context = _serviceProvider.GetRequiredService<IApplicationDbContext>();
        _journal = new DbJournal(username, context);
        return _journal;       
    }

    public IJournal Get()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(DbJournalFactory));
        return _journal
            ?? throw new ArgumentNullException("Journal is not created.");
    }

    public void Dispose() 
    {
        if (_disposed)
            return;
        _disposed = true;
        _journal = null;
    }
}
