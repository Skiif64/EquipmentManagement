using Dapper;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EquipmentManagement.DAL.Repositories;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    private bool _disposed;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Employee entity, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        await _context.Employees.AddAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(Employee entity, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        _context.Employees.Remove(entity);
    }

    public void Dispose()
    {
        if (_disposed)
            return;
        _disposed = true;
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;
        _disposed = true;
        await _context.DisposeAsync();        
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        return _context.Employees;
    }    

    public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            ?? throw new Exception();
    }

    public async Task UpdateAsync(Employee entity, CancellationToken cancellationToken)
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().Name);

        _context.Employees.Update(entity);
    }
}
