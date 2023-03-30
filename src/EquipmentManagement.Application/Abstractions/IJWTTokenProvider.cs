using EquipmentManagement.Application.Models;

namespace EquipmentManagement.Application.Abstractions;

public interface IJwtTokenProvider
{
    string Generate(ApplicationUser user);
}
