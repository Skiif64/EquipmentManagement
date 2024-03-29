﻿using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.Equipments.Create;
using EquipmentManagement.Application.Equipments.Delete;
using EquipmentManagement.Application.Equipments.Get;
using EquipmentManagement.Application.Equipments.GetByEmployeeId;
using EquipmentManagement.Application.Equipments.GetById;
using EquipmentManagement.Application.Equipments.GetFree;
using EquipmentManagement.Application.Equipments.Update;
using EquipmentManagement.Application.Statuses.GetOrCreate;
using EquipmentManagement.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EquipmentController : ControllerBase
{
    private const string DefaultStatusName = "Создано";
    private readonly ISender _sender;
    private readonly IMapper _mapper;   

    public EquipmentController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetAsync(int? page, int? pageSize, string? order, string? criteria, string? filter, CancellationToken cancellationToken)
    {
        var equipments = await _sender.Send(new GetEquipmentDtoQuery(page, pageSize, order, criteria, filter), cancellationToken);
        var response = _mapper.Map<PagedListResponse<EquipmentResponse>>(equipments);
        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EquipmentResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var equipment = await _sender.Send(new GetEquipmentByIdQuery(id), cancellationToken);
        var response = _mapper.Map<EquipmentResponse>(equipment);
        return Ok(response);
    }

    [HttpGet("free")]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetFreeAsync(CancellationToken cancellationToken)
    {
        var query = new GetFreeEquipmentQuery();
        var equipments = await _sender.Send(query, cancellationToken);

        var response = _mapper.Map<IEnumerable<EquipmentResponse>>(equipments);
        return Ok(response);
    }

    [HttpPost("add")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<Guid>> AddEquipmentAsync(AddEquipmentRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var author = User.Identity?.Name;
        request.Author = author ?? "неизвестно";
        var command = _mapper.Map<CreateEquipmentCommand>(request);
        var id = await _sender.Send(command, cancellationToken); 
       
        return Ok(id);
    }

    [HttpGet("employee/{employeeId:guid}")]
    public async Task<ActionResult<IEnumerable<EquipmentResponse>>> GetByEmployeeId(Guid employeeId, CancellationToken cancellationToken)
    {
        var query = new GetEquipmentsByEmployeeIdQuery(employeeId);
        var equipments = await _sender.Send(query, cancellationToken);
        var response = Enumerable.Empty<EquipmentResponse>();
        if (equipments is not null && equipments.Any())
            response = _mapper.Map<IEnumerable<EquipmentResponse>>(equipments);
        return Ok(response);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(UpdateEquipmentRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateEquipmentCommand>(request);
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteEquipmentByIdCommand(id);

        await _sender.Send(command, cancellationToken);
        return NoContent();
    }
}
