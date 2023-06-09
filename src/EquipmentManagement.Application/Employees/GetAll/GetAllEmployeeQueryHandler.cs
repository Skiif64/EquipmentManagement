﻿using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.Employees.GetAll;

public class GetAllEmployeeQueryHandler : IQueryHandler<GetAllEmployeeQuery, IEnumerable<Employee>?>
{
    private readonly IApplicationDbContext _context;

    public GetAllEmployeeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Employee>?> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        var set = _context
            .Set<Employee>()
            .OrderBy(x => x.Status)
            .ThenBy(x => x.Lastname)
            .ThenBy(x => x.Firstname)            
            ;
        return Task.FromResult(set?.AsEnumerable());
    }
}
