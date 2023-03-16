using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Auth;

public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyAuthorizationFilter))
    {
    }
}
