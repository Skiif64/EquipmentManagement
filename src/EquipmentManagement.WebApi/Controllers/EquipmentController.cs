using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Application.Equipments.GetAll;
using EquipmentManagement.Application.Equipments.GetByEmployeeId;
using EquipmentManagement.Auth;
using EquipmentManagement.Domain.Models;
using EquipmentManagement.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EquipmentController : ControllerBase
{        
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public EquipmentController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var equipments = await _sender.Send(new GetAllEquipmentQuery(), cancellationToken);
        return Ok(equipments);
    }

    [HttpPost("add")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> AddEquipmentAsync(AddEquipmentRequest request, CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<AddEquipmentCommand>(request);
        var id = await _sender.Send(command, cancellationToken);
        return Ok(id);
    }

    [HttpGet("employee/{employeeId:guid}")]
    public async Task<IActionResult> GetByEmployeeId(Guid employeeId, CancellationToken cancellationToken)
    {
        var query = new GetEquipmentsByEmployeeIdQuery(employeeId);
        var equipments = await _sender.Send(query, cancellationToken);
        return Ok(equipments);
    }
}
