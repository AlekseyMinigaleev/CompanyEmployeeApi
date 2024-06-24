using CompanyEmployeeApi.Features.Employee.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeApi.Features.Employee
{
    /// <summary>
    /// Контроллер для управления сотрудниками.
    /// </summary>
    public class EmployeeController(IEmployeeCrudService employeeService) : BaseApiController
    {
        private readonly IEmployeeCrudService _employeeService = employeeService;

        /// <summary>
        /// Получает список всех сотрудников.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список всех сотрудников.</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
            Ok(await _employeeService.GetAllAsync(cancellationToken));

        /// <summary>
        /// Получает информацию о сотруднике по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о сотруднике или NotFound, если сотрудник не найден.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService
                .GetByIdAsync(id, cancellationToken);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        /// <summary>
        /// Создает нового сотрудника.
        /// </summary>
        /// <param name="createEmployeeVM">ViewModel для создания сотрудника.</param>
        /// <param name="validator">Валидатор для ViewModel создания сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданный сотрудник.</returns>
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

        /// <summary>
        /// Удаляет сотрудника по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Удаленный сотрудник или NotFound, если сотрудник не найден.</returns>
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

        /// <summary>
        /// Обновляет информацию о сотруднике по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <param name="updateEmployeeVm">ViewModel для обновления информации о сотруднике.</param>
        /// <param name="validator">Валидатор для ViewModel обновления информации о сотруднике.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Обновленная информация о сотруднике или NotFound, если сотрудник не найден.</returns>
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