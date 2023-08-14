using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquimentManagement.Contracts.Responses;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.Employees.Delete;
using EquipmentManagement.Application.Employees.Fire;
using EquipmentManagement.Application.Employees.GetAll;
using EquipmentManagement.Application.Employees.GetById;
using EquipmentManagement.Application.Employees.Restore;
using EquipmentManagement.Application.Employees.Update;
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
        public EmployeesController(ISender sender, IMapper mapper, ILogger<EmployeesController>? logger = null)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;           
        }
        [HttpPost("add")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Guid>> AddAsync(AddEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var command = _mapper.Map<AddEmployeeCommand>(request);
            var id = await _sender.Send(command, cancellationToken);

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
            var employee = await _sender.Send(new GetEmployeeByIdQuery(id), cancellationToken);
            var response = _mapper.Map<EmployeeResponse>(employee);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteEmployeeCommand(id);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateEmployeeCommand>(request);
            await _sender.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPatch("delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Guid>> DeleteByIdAsync(FireEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = new FireEmployeeCommand(request.EmployeeId);
            var resultId = await _sender.Send(command, cancellationToken);
            return Ok(resultId);
        }

        [HttpPatch("restore")]
        public async Task<ActionResult<Guid>> RestoreByIdAsync(RestoreEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = new RestoreEmployeeCommand(request.EmployeeId);
            var resultId = await _sender.Send(command, cancellationToken);
            return Ok(resultId);
        }
    }
}
