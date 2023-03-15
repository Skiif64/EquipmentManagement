using Dapper;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;
using System.Data;

namespace EquipmentManagement.DAL.Repositories;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnection _connection;
    private bool _disposed;

    public EmployeeRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task CreateAsync(Employee entity, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        const string sql =
            "INSERT INTO Employees (Id, Firstname, Lastname, Patronymic) " +
            "VALUES (@Id, @Firstname, @Lastname, @Patronymic)";
        await _connection.ExecuteAsync(sql, entity);
    }

    public async Task DeleteAsync(Employee entity, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        const string sql =
            "DELETE FROM Employees WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, entity);
    }

    public void Dispose()
    {
        if (_disposed)
            return;
        _disposed = true;
        _connection?.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        if (_disposed)
            return ValueTask.CompletedTask;
        _disposed = true;
        _connection.Dispose();
        return ValueTask.CompletedTask;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        const string sql =
            "SELECT * FROM Employees";
        var result = await _connection.QueryAsync<Employee>(sql);
        return result;
    }    

    public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        const string sql =
            "SELECT * FROM Employees WHERE Id = @Id";
        var result = await _connection.QueryFirstAsync<Employee>(sql, id);
        return result;
    }

    public async Task UpdateAsync(Employee entity, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        const string sql =
            "UPDATE Employees " +
            "SET Firstname = @Firstname, Lastname = @Lastname, Patronymic = @Patronymic";
        await _connection.ExecuteAsync(sql, entity);
    }
}
