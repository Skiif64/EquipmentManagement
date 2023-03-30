using AutoMapper;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application.Journaling.GetAll;
using EquipmentManagement.Application.Journaling.GetWithPaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JournalController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public JournalController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("/all")]
    public async Task<ActionResult<IEnumerable<JournalRecordResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllJournalQuery();
        var result = await _sender.Send(query, cancellationToken);

        var response = _mapper.Map<IEnumerable<JournalRecordResponse>>(result);
        return Ok(response);
    }

    [HttpGet("/{count:int}/{offset:int}")]
    public async Task<ActionResult<IEnumerable<JournalRecordResponse>>> GetAsync(int count, int offset, CancellationToken cancellationToken)
    {
        var query = new GetLastWithPagingJournalQuery(count, offset);
        var result = await _sender.Send(query, cancellationToken);

        var response = _mapper.Map<IEnumerable<JournalRecordResponse>>(result);
        return Ok(response);
    }
}
