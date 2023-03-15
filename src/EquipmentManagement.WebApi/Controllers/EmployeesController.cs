using AutoMapper;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.AddEmployee;
using EquipmentManagement.Domain.Abstractions.Repositories;
using EquipmentManagement.WebApi.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> AddAsync(AddEmployeeRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddEmployeeCommand>(request);
            await _sender.Send(command, cancellationToken);
            return Ok();
        }
    }
}
