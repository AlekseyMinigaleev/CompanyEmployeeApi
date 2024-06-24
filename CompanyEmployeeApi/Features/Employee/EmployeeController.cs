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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var deletedEmployee = await _employeeService
                .DeleteByIdAsync(id, cancellationToken);

            if (deletedEmployee is null)
                return NotFound();

            return Ok(deletedEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(
            Guid id,
            [FromBody] UpdateEmployeeViewModel updateEmployeeVm,
            [FromServices] IValidator<UpdateEmployeeViewModel> validator,
            CancellationToken cancellationToken)
        {
            updateEmployeeVm.EmployeeId = id;

            await ValidateAndChangeModelStateAsync(
                validator,
                updateEmployeeVm,
                cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedEmployee = await _employeeService
                .UpdateByIdAsync(updateEmployeeVm, cancellationToken);

            if (updatedEmployee is null)
                return NotFound();

            return CreatedAtAction(
                nameof(GetById),
                new { id = updatedEmployee.Id },
                updatedEmployee);
        }
    }
}