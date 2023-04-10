﻿using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Application.Equipments.Get;
using EquipmentManagement.Application.Equipments.GetAll;
using EquipmentManagement.Application.Equipments.GetAllWithStatus;
using EquipmentManagement.Application.Equipments.GetByEmployeeId;
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
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IJournal _journal;

    public EquipmentController(ISender sender, IMapper mapper, IJournal journal)
    {
        _sender = sender;
        _mapper = mapper;
        _journal = journal;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentWithStatusResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var equipments = await _sender.Send(new GetAllEquipmentWithStatusQuery(), cancellationToken);
        var response = _mapper.Map<IEnumerable<EquipmentWithStatusResponse>>(equipments);
        return Ok(response);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EquipmentResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var equipment = await _sender.Send(new GetEquipmentByIdQuery(id), cancellationToken);
        var response = _mapper.Map<EquipmentResponse>(equipment);
        return Ok(response);
    }

    [HttpPost("add")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<Guid>> AddEquipmentAsync(AddEquipmentRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<AddEquipmentCommand>(request);
        var id = await _sender.Send(command, cancellationToken);
        var author = User.Identity?.Name;

        var recordCommand = new AddEquipmentRecordCommand
        {
            EquipmentId = id,
            ModifyAuthor = author ?? "неизвестно",
            StatusId
        }

        await _journal.WriteAsync(AppLogEvents.Create,
       $"Добавлено оборудование {request.Type} {request.Article} {request.SerialNumber}",
       author,
       DateTimeOffset.UtcNow,
       cancellationToken);

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
}
