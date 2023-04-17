using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.EquipmentRecords.GetActualsByEmployeeID;
using EquipmentManagement.Application.EquipmentRecords.GetAll;
using EquipmentManagement.Application.EquipmentRecords.GetByEquipmentId;
using EquipmentManagement.Application.EquipmentRecords.Update;
using EquipmentManagement.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EquipmentRecordsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;    

    public EquipmentRecordsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;       
    }

    [HttpPost("add")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<Guid>> AddEquipmentRecord(AddEquipmentRecordRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var author = User.Identity?.Name;
        request.ModifyAuthor = author ?? "неизвестно";
        var command = _mapper.Map<AddEquipmentRecordCommand>(request);
        var id = await _sender.Send(command, cancellationToken);        

        return Ok(id);
    }

    [HttpPatch("update")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<EquipmentRecordResponse>> UpdateEquipmentRecord(UpdateEquipmentRecordRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<UpdateEquipmentRecordCommand>(request);
        var record = await _sender.Send(command, cancellationToken);
        var response = _mapper.Map<EquipmentRecordResponse>(record);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentRecordResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllEquipmentRecordsQuery();
        var records = await _sender.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<EquipmentRecordResponse>>(records);
        return Ok(response);
    }

    [HttpGet("equipment/{equipmentId:guid}")]
    public async Task<ActionResult<IEnumerable<EquipmentRecordResponse>>> GetByEquipmentIdAsync(Guid equipmentId, CancellationToken cancellationToken)
    {
        var query = new GetEquipmentRecordByEquipmentIdQuery(equipmentId);
        var records = await _sender.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<EquipmentRecordResponse>>(records);
        return Ok(response);
    }

    [HttpGet("{employeeId:guid}")]
    public async Task<ActionResult<IEnumerable<EquipmentRecordResponse>>> GetActualEquipmentRecords(Guid employeeId, CancellationToken cancellationToken)
    {
        var command = new GetActualsByEmployeeIdQuery(employeeId);
        var records = await _sender.Send(command, cancellationToken);
        var response = _mapper.Map<IEnumerable<EquipmentRecordResponse>>(records);
        return Ok(response);
    }
}
