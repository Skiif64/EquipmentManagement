using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application.EquipmentTypes.Add;
using EquipmentManagement.Application.EquipmentTypes.GetAll;
using EquipmentManagement.Application.EquipmentTypes.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TypeController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentTypeResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllEquipmentTypeQuery();
        var types = await _sender.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<EquipmentTypeResponse>>(types);
        return Ok(response);        
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EquipmentTypeResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEquipmentTypeByIdQuery(id);
        var type = await _sender.Send(query, cancellationToken);
        var response = _mapper.Map<EquipmentTypeResponse>(type);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddAsync(AddEquipmentTypeRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<AddEquipmentTypeCommand>(request);
        var id = await _sender.Send(command, cancellationToken);
        return Ok(id);
    }
}
