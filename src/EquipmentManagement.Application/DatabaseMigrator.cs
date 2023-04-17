using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application;

internal class DatabaseMigrator : IDatabaseMigrator
{
    private readonly IEnumerable<IMigrationableDatabase> _databases;

    public DatabaseMigrator(IEnumerable<IMigrationableDatabase> databases)
    {
        _databases = databases;
    }

    public void Invoke()
    {
        foreach (var database in _databases)
        {
            database.Migrate();
        }
    }

    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        foreach (var database in _databases)
        {
            await database.MigrateAsync(cancellationToken);
        }
    }
}
