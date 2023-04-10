using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.Employees.Delete;
using EquipmentManagement.Application.Employees.Get;
using EquipmentManagement.Application.Employees.GetAll;
using EquipmentManagement.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EquipmentManagement.UI.Utils.PagePaths;

namespace EquipmentManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController>? _logger;
        private readonly IJournal _journal;
        public EmployeesController(ISender sender, IMapper mapper, IJournal journal, ILogger<EmployeesController>? logger = null)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
            _journal = journal;
        }
        [HttpPost("add")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Guid>> AddAsync(AddEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var command = _mapper.Map<AddEmployeeCommand>(request);
            var id = await _sender.Send(command, cancellationToken);

            var author = User.Identity?.Name;
            await _journal.WriteAsync(AppLogEvents.Create,
           $"Добавлен сотрудник {request.Lastname} {request.Firstname} {request.Patronymic}",
           author,
           DateTimeOffset.UtcNow,
           cancellationToken);

            return Ok(id);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var employees = await _sender.Send(new GetAllEmployeeQuery(), cancellationToken);
            var response = _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
            return Ok(response);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _sender.Send(new GetEmployeeQuery(id), cancellationToken);
            var response = _mapper.Map<EmployeeResponse>(employee);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Guid>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteEmployeeCommand(id);
            var resultId = await _sender.Send(command, cancellationToken);
            return Ok(resultId);
        }
    }
}
