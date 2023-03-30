using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.EntityFrameworkCore;


namespace EquipmentManagement.Auth;

public class JwtAuthentificationService
{
    private readonly UsersDbContext _context;
    private readonly IJwtTokenProvider _tokenProvider;

    public JwtAuthentificationService(UsersDbContext context, IJwtTokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }


    public async Task<AuthentificationResult> SignInAsync(string login, string password, CancellationToken cancellationToken)
    {
        var user = await _context
            .Set<ApplicationUser>()
            .SingleOrDefaultAsync(x => x.Login == login, cancellationToken);
        if (user is null)
            return AuthentificationResult.CreateFailure(new[] 
            { KeyValuePair.Create("NotFound", "Invalid login.") });
        if(!user.PasswordEquals(password))
            return AuthentificationResult.CreateFailure(new[] 
            { KeyValuePair.Create("InvalidPassword", "Invalid password.") });

        var token = _tokenProvider.Generate(user);

        var newRefreshToken = Guid.NewGuid();
        user.RefreshToken = newRefreshToken;
        _context
            .Set<ApplicationUser>()
            .Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return AuthentificationResult.CreateSuccess(token, newRefreshToken.ToString());
    }

    public async Task<AuthentificationResult> RegisterAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var existingUser = await _context
            .Set<ApplicationUser>()
            .SingleOrDefaultAsync(x => x.Login == user.Login, cancellationToken);
        if (existingUser is not null)
            return AuthentificationResult.CreateFailure(
                new[] { KeyValuePair.Create<string, string>("LoginExists", "User with that login already exists.") });
        await _context
            .Set<ApplicationUser>()
            .AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return AuthentificationResult.CreateSuccess("");
    }

    public async Task LogoutAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _context
            .Set<ApplicationUser>()
            .FindAsync(new object[] { userId }, cancellationToken);
        if (user is null)
            return;
        user.RefreshToken = null;
        _context
            .Set<ApplicationUser>()
            .Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuthentificationResult> RefreshAccessTokenAsync(Guid refreshToken, CancellationToken cancellationToken)
    {
        var user = await _context
            .Set<ApplicationUser>()
            .SingleOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);
        if (user is null)
            return AuthentificationResult.CreateFailure(new[]
            { KeyValuePair.Create("NotFound", "Invalid refresh token.") });

        var newAccessToken = _tokenProvider.Generate(user);
        var newRefreshToken = Guid.NewGuid();
        user.RefreshToken = newRefreshToken;
        _context
            .Set<ApplicationUser>()
            .Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return AuthentificationResult.CreateSuccess(newAccessToken, newRefreshToken.ToString());
    }
}
