using AutoMapper;
using EquipmentManagement.Application.EquipmentRecords.Add;
using EquipmentManagement.Application.EquipmentRecords.GetActualsByEmployeeID;
using EquipmentManagement.Application.EquipmentRecords.Update;
using EquipmentManagement.Application.Equipments.GetByEmployeeId;
using EquipmentManagement.Auth;
using EquipmentManagement.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.UserAdmin)]
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
    public async Task<IActionResult> AddEquipmentRecord(AddEquipmentRecordRequest request, CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<AddEquipmentRecordCommand>(request);
        var id = await _sender.Send(command, cancellationToken);
        return Ok(id);
    }

    [HttpPatch("update")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> UpdateEquipmentRecord(UpdateEquipmentRecordRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = _mapper.Map<UpdateEquipmentRecordCommand>(request);
        var record = await _sender.Send(command, cancellationToken);
        return Ok(record);
    }

    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetActualEquipmentRecords(Guid employeeId, CancellationToken cancellationToken)
    {
        var command = new GetActualsByEmployeeIdQuery(employeeId);
        var records = await _sender.Send(command, cancellationToken);
        return Ok(records);
    }
}
