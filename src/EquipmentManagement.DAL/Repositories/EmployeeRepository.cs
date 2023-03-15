using Dapper;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EquipmentManagement.DAL.Repositories;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;      

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;        
    }
    public async Task CreateAsync(Employee entity, CancellationToken cancellationToken)
    {        
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee entity, CancellationToken cancellationToken)
    {        
        _context.Employees.Remove(entity);
        await _context.SaveChangesAsync();
    }    

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {        
        return _context.Employees;
    }    

    public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {  
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            ?? throw new Exception();
    }

    public async Task UpdateAsync(Employee entity, CancellationToken cancellationToken)
    {
        _context.Employees.Update(entity);
        await _context.SaveChangesAsync();
    }
}
