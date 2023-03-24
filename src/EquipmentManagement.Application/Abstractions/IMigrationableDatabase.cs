namespace EquipmentManagement.Application.Abstractions;

public interface IMigrationableDatabase
{
    void Migrate();
    Task MigrateAsync(CancellationToken cancellationToken);
}
