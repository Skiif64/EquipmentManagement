﻿using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Equipments.Add;
using EquipmentManagement.Application.Equipments.GetAll;
using EquipmentManagement.Application.Equipments.GetByEmployeeId;
using EquipmentManagement.Domain.Models;
using EquipmentManagement.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> AddEquipmentAsync(AddEquipmentRequest request, CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<AddEquipmentCommand>(request);
        await _sender.Send(command, cancellationToken);
        return Ok();
    }
}