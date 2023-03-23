using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
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
    public StatusController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
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
    public async Task<ActionResult<Guid>> AddAsync(AddStatusRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<AddStatusCommand>(request);
        var id = await _sender.Send(command, cancellationToken);
        return Ok(id);
    }
}
