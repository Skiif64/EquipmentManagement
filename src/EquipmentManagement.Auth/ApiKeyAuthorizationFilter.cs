using EquipmentManagement.Auth.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EquipmentManagement.Auth;

public class ApiKeyAuthorizationFilter : IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-Api-Key";
    private readonly IApiKeyValidator _validator;

    public ApiKeyAuthorizationFilter(IApiKeyValidator validator)
    {
        _validator = validator;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
        if (!_validator.IsValid(apiKey))
            context.Result = new UnauthorizedResult();
    }
}
