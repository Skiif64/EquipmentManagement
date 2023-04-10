using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Statuses.Add;
using EquipmentManagement.Application.Statuses.Get;
using EquipmentManagement.Application.Statuses.GetAll;
using EquipmentManagement.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StatusController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IJournal _journal;
    public StatusController(ISender sender, IMapper mapper, IJournal journal)
    {
        _sender = sender;
        _mapper = mapper;
        _journal = journal;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatusResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var statuses = await _sender.Send(new GetAllStatusQuery(), cancellationToken);
        var response = _mapper.Map<IEnumerable<StatusResponse>>(statuses);
        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StatusResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var status = await _sender.Send(new GetStatusByIdQuery(id), cancellationToken);
        var response = _mapper.Map<StatusResponse>(status);
        return Ok(response);
    }
    [HttpPost("add")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<StatusResponse>> AddAsync(AddStatusRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<AddStatusCommand>(request);
        var status = await _sender.Send(command, cancellationToken);
        var response = _mapper.Map<StatusResponse>(status);

        var author = User.Identity?.Name;
        await _journal.WriteAsync(AppLogEvents.Create,
       $"Добавлен статус: {request.Title}",
       author,
       DateTimeOffset.UtcNow,
       cancellationToken);

        return Ok(response);
    }
}
