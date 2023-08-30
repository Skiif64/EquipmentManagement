using EquipmentManagement.Application.Abstractions;

namespace EquipmentManagement.Application;

internal class DatabaseMigrator : IDatabaseMigrator
{
    private readonly IEnumerable<IDatabaseSeeder> _seeders;

    public DatabaseMigrator(IEnumerable<IDatabaseSeeder> seeders)
    {
        _seeders = seeders;
    }

    public void Invoke()
    {
        foreach (var database in _seeders)
        {
            database.Seed();
        }
    }

    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync(cancellationToken);
        }
    }
}
