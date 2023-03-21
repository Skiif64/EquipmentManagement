using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Auth.Abstractions;

public interface IJwtTokenProvider
{
    string Generate(ApplicationUser user);
}
