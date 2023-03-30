﻿using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.ApplicationUsers.Register;
internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IUserDbContext _context;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context
            .Set<ApplicationUser>()
            .SingleOrDefaultAsync(x => x.Login == request.Login, cancellationToken);
        if (existingUser is not null)
            return AuthenticationResult.CreateFailure(
                new[] { KeyValuePair.Create("LoginExists", "User with that login already exists.") });
        var user = _mapper.Map<ApplicationUser>(request);
        await _context
            .Set<ApplicationUser>()
            .AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return AuthenticationResult.CreateSuccess("");
    }
}
