﻿using AutoMapper;
using EquipmentManagement.Application.Employees.Add;
using EquipmentManagement.Application.Employees.Get;
using EquipmentManagement.Application.Employees.GetAll;
using EquipmentManagement.WebApi.Requests;
using MediatR;
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
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _sender.Send(new GetAllEmployeeQuery(), cancellationToken);
            return Ok(employees);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _sender.Send(new GetEmployeeQuery(id), cancellationToken);
            return Ok(employee);
        }
    }
}
