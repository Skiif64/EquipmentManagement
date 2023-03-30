using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.ApplicationUsers.SignIn;
internal class SignInCommandHandler : ICommandHandler<SignInCommand, AuthenticationResult>
{
    private readonly IUserDbContext _context;
    private readonly IJwtTokenProvider _tokenProvider;

    public SignInCommandHandler(IUserDbContext context, IJwtTokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthenticationResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Set<ApplicationUser>()
            .SingleOrDefaultAsync(x => x.Login == request.Login, cancellationToken);
        if (user is null)
            return AuthenticationResult.CreateFailure(new[]
            { KeyValuePair.Create("NotFound", "Invalid login.") });
        if (!user.PasswordEquals(request.Password))
            return AuthenticationResult.CreateFailure(new[]
            { KeyValuePair.Create("InvalidPassword", "Invalid password.") });

        var token = _tokenProvider.Generate(user);

        var newRefreshToken = Guid.NewGuid();
        user.RefreshToken = newRefreshToken;
        _context
            .Set<ApplicationUser>()
            .Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return AuthenticationResult.CreateSuccess(token, newRefreshToken.ToString());
    }
}
