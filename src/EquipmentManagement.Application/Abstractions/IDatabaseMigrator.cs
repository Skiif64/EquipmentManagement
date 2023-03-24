namespace EquipmentManagement.Application.Abstractions
{
    public interface IDatabaseMigrator
    {
        void Invoke();
        Task InvokeAsync(CancellationToken cancellationToken);
    }
}