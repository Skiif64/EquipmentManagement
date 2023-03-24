using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly JwtAuthentificationService _jwtAuthentificationService;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthController> _logger;

    public AuthController(JwtAuthentificationService jwtAuthentificationService, ILogger<AuthController> logger, IMapper mapper)
    {
        _jwtAuthentificationService = jwtAuthentificationService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = await _jwtAuthentificationService.SignInAsync(request.Login, request.Password, cancellationToken);
            
        _logger.LogInformation(AppLogEvents.Login, "User {username} is logged in", request.Login);
        var response = _mapper.Map<AuthentificationResponse>(result);
        return result.IsSuccess
            ? Ok(response) 
            : BadRequest();
    }

    [HttpPost("register")]
    [Authorize(Roles = Roles.Admin)]    
    public async Task<IActionResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var user = _mapper.Map<ApplicationUser>(request);

        await _jwtAuthentificationService.RegisterAsync(user, cancellationToken);

        _logger.LogInformation(AppLogEvents.Register, "User {username} is registered", request.Login);
        return Ok();
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {          
        return Ok();
    }
}
