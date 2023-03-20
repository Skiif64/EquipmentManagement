using EquipmentManagement.Application.Models;
using EquipmentManagement.Auth.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace EquipmentManagement.Auth;

public class JwtAuthentificationService
{
    private readonly UserStoreDbContext _userStoreDbContext;
    private readonly IJwtTokenProvider _tokenProvider;

    public JwtAuthentificationService(UserStoreDbContext userStoreDbContext, IJwtTokenProvider tokenProvider)
    {
        _userStoreDbContext = userStoreDbContext;
        _tokenProvider = tokenProvider;
    }


    public async Task<AuthentificationResult> SignInAsync(string login, string password, CancellationToken cancellationToken)
    {
        var user = await _userStoreDbContext
            .Set<ApplicationUser>()
            .SingleOrDefaultAsync(x => x.Login == login, cancellationToken);
        if (user is null)
            return new AuthentificationResult();

        var token = _tokenProvider.Generate(user);

        return new AuthentificationResult(token);
    }
}
