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

    {
    }
}