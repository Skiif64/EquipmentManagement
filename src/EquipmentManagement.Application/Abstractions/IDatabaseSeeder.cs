namespace EquipmentManagement.Application.Abstractions;

public interface IDatabaseSeeder
{
    void Seed();
    Task SeedAsync(CancellationToken cancellationToken);
}
