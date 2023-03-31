using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.ApplicationUsers.GetAll;
using EquipmentManagement.Application.ApplicationUsers.Logout;
using EquipmentManagement.Application.ApplicationUsers.RefreshAccessToken;
using EquipmentManagement.Application.ApplicationUsers.Register;
using EquipmentManagement.Application.ApplicationUsers.SignIn;
using EquipmentManagement.Application.Models;
using EquipmentManagement.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthController> _logger;
    private readonly IJournal _journal;

    public AuthController(ISender sender, ILogger<AuthController> logger, IMapper mapper, IJournal journal)
    {
        _sender = sender;
        _logger = logger;
        _mapper = mapper;
        _journal = journal;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthentificationResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = _mapper.Map<SignInCommand>(request);
        var result = await _sender.Send(command, cancellationToken);

        _logger.LogInformation(AppLogEvents.Login, "User {username} is logged in", request.Login);
        var response = _mapper.Map<AuthentificationResponse>(result);
        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpPost("register")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<AuthentificationResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = _mapper.Map<RegisterCommand>(request);
        var response = await _sender.Send(command, cancellationToken);
        if (response.IsSuccess)
        {
            _logger.LogInformation(AppLogEvents.Register, "User {username} is registered", request.Login);
            var username = User.Identity?.Name;
            await _journal.WriteAsync(AppLogEvents.Register,
                $"Пользователь {request.Login} зарегистрирован",
               username,
               DateTimeOffset.UtcNow,
                cancellationToken);
        }
        else
            _logger.LogInformation(AppLogEvents.Register, "User {username} is already exists", request.Login);
        return response.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("refresh/{refreshToken:guid}")]
    public async Task<ActionResult<AuthentificationResponse>> RefreshAccessTokenAsync(Guid refreshToken, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new RefreshAccessTokenCommand(refreshToken);
        var result = await _sender.Send(command, cancellationToken);
        var response = _mapper.Map<AuthentificationResponse>(result);
        return result.IsSuccess
            ? Ok(response)
            : BadRequest(response);
    }

    [HttpGet("logout/{userId:guid}")]
    public async Task<IActionResult> LogoutAsync(Guid userId, CancellationToken cancellationToken)
    {
        var command = new LogoutCommand(userId);
        await _sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<ApplicationUserResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var users = await _sender.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<ApplicationUserResponse>>(users);
        return Ok(response);
    }
}
