using CompanyEmployeeApi.Features.Employee.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeApi.Features.Employee
{
    public class EmployeeController(EmployeeService employeeService) : BaseApiController
    {
        private readonly EmployeeService _employeeService = employeeService;

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
            Ok(await _employeeService.GetAllAsync(cancellationToken));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService
                .GetByIdAsync(id, cancellationToken);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(
            CreateEmployeeViewModel createEmployeeVM,
            [FromServices] IValidator<CreateEmployeeViewModel> validator,
            CancellationToken cancellationToken)
        {
            await ValidateAndChangeModelStateAsync(validator, createEmployeeVM, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdEmployee = await _employeeService
                .CreateAsync(createEmployeeVM, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEmployee.Id },
                createdEmployee);
        }
    }
}