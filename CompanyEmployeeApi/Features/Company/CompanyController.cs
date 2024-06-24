using CompanyEmployeeApi.Features.Company.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeApi.Features.Company
{
    public class CompanyController(ICompanyService companyService) : BaseApiController
    {
        private readonly ICompanyService _companyService = companyService;

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var companies = await _companyService
                .GetAllAsync(cancellationToken);

            return Ok(companies);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var deletedCompany = await _companyService
                .DeleteByIdsAsync(id, cancellationToken);

            if (deletedCompany is null)
                return NotFound();

            return Ok(deletedCompany);
        }

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
                .UpdateByIdAsync(id, companyVM, cancellationToken);

            if (updatedCompany is null)
                return NotFound();

            return CreatedAtAction(
                nameof(GetById),
                new { id = updatedCompany.Id },
                updatedCompany);
        }
    }
}