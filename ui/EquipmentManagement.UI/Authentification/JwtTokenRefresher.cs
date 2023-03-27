using EquipmentManagement.UI.Abstractions;

namespace EquipmentManagement.UI.Authentification;

public class JwtTokenRefresher : IJwtTokenRefresher
{
    private readonly HttpClient _client;
    private readonly ITokenStorage _storage;

    public JwtTokenRefresher(HttpClient client, ITokenStorage storage)
    {
        _client = client;
        _storage = storage;
    }

    public Task RefreshAccessToken(string refreshToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
