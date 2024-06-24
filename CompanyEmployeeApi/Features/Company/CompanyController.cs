using CompanyEmployeeApi.Features.Company.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeApi.Features.Company
{
    /// <summary>
    /// Контроллер для операций с компаниями.
    /// </summary>
    public class CompanyController(ICompanyCrudService companyService) : BaseApiController
    {
        private readonly ICompanyCrudService _companyService = companyService;

        /// <summary>
        /// Получает все компании.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список всех компаний.</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var companies = await _companyService
                .GetAllAsync(cancellationToken);

            return Ok(companies);
        }

        /// <summary>
        /// Получает компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Запись компании или 404, если не найдена.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var company = await _companyService
                .GetByIdAsync(id, cancellationToken);

            if (company is null)
                return NotFound();

            return Ok(company);
        }

        /// <summary>
        /// Создает новую компанию.
        /// </summary>
        /// <param name="companyVM">ViewModel для создания компании.</param>
        /// <param name="validator">Валидатор для ViewModel создания компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданная запись компании.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany(
            [FromBody] CreateCompanyViewModel companyVM,
            [FromServices] IValidator<CreateCompanyViewModel> validator,
            CancellationToken cancellationToken)
        {
            await ValidateAndChangeModelStateAsync(validator, companyVM, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCompany = await _companyService
                .CreateAsync(companyVM, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCompany.Id },
                createdCompany);
        }

        /// <summary>
        /// Удаляет компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Удаленная запись компании или 404, если не найдена.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var deletedCompany = await _companyService
                .DeleteByIdAsync(id, cancellationToken);

            if (deletedCompany is null)
                return NotFound();

            return Ok(deletedCompany);
        }

        /// <summary>
        /// Обновляет компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании.</param>
        /// <param name="companyVM">ViewModel для обновления компании.</param>
        /// <param name="validator">Валидатор для ViewModel обновления компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Обновленная запись компании или 404, если не найдена.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(
            Guid id,
            [FromBody] UpdateCompanyViewModel companyVM,
            [FromServices] IValidator<UpdateCompanyViewModel> validator,
            CancellationToken cancellationToken)
        {
            companyVM.CompanyId = id;

            await ValidateAndChangeModelStateAsync(validator, companyVM, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCompany = await _companyService
                .UpdateByIdAsync(companyVM, cancellationToken);

            if (updatedCompany is null)
                return NotFound();

            return CreatedAtAction(
                nameof(GetById),
                new { id = updatedCompany.Id },
                updatedCompany);
        }
    }
}