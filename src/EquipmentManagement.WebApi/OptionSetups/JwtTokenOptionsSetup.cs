using EquipmentManagement.Auth;
using Microsoft.Extensions.Options;

namespace EquipmentManagement.WebApi.OptionSetups;

public class JwtTokenOptionsSetup : IConfigureOptions<JwtTokenOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtTokenOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtTokenOptions options)
    {
        _configuration.GetRequiredSection(SectionName).Bind(options);
    }
}
