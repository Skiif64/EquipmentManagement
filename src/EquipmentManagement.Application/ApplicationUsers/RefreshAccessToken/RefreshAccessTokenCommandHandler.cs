using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application.ApplicationUsers.RefreshAccessToken;
internal class RefreshAccessTokenCommandHandler : ICommandHandler<RefreshAccessTokenCommand, AuthenticationResult>
{
    private readonly IUserDbContext _context;
    private readonly IJwtTokenProvider _tokenProvider;

    public RefreshAccessTokenCommandHandler(IUserDbContext context, IJwtTokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthenticationResult> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Set<ApplicationUser>()
            .SingleOrDefaultAsync(x => x.RefreshToken == request.RefreshToken, cancellationToken);
        if (user is null)
            return AuthenticationResult.CreateFailure(new[]
            { KeyValuePair.Create("NotFound", "Invalid refresh token.") });

        var newAccessToken = _tokenProvider.Generate(user);
        var newRefreshToken = Guid.NewGuid();
        user.RefreshToken = newRefreshToken;
        _context
            .Set<ApplicationUser>()
            .Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return AuthenticationResult.CreateSuccess(newAccessToken, newRefreshToken.ToString());
    }
}
