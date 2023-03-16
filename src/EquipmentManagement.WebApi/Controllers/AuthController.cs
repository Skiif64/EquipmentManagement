using EquipmentManagement.Auth;
using EquipmentManagement.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<AuthController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(request.Login, request.Password, request.RememberMe, false);
        if(!result.Succeeded)
            return BadRequest();    
        _logger.LogInformation("User {username} is logged in", request.Login);
        return Ok();
    }

    [HttpPost("register")]
    [Authorize(Roles = Roles.Admin)]    
    public async Task<IActionResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var user = new IdentityUser(request.Login);
        
        var result = await _userManager.CreateAsync(user, request.Password);
        if(result.Succeeded)
            result = await _userManager.AddToRoleAsync(user, request.Role);
        if(!result.Succeeded)
            return BadRequest(result.Errors);
        _logger.LogInformation("User {username} is registered", request.Login);
        return Ok();
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();        
        return Ok();
    }
}
