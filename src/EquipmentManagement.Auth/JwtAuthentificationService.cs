using EquipmentManagement.Application.Models;
using EquipmentManagement.Auth.Abstractions;
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
            return AuthentificationResult.CreateFailure(new[] { KeyValuePair.Create("NotFound", "Invalid login.") });

        var token = _tokenProvider.Generate(user);

        return AuthentificationResult.CreateSuccess(token);
    }

    public async Task<AuthentificationResult> RegisterAsync(ApplicationUser user, CancellationToken cancellationToken)
    {        
        await _context
            .Set<ApplicationUser>()
            .AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return AuthentificationResult.CreateSuccess("");
    }
}
