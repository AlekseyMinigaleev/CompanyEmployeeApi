using CompanyEmployeeApi.Features.Compnay.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeApi.Features.Company
{
    public class CompanyController(ICompanyService companyService) : BaseApiController
    {
        private readonly ICompanyService _companyService = companyService;

        [HttpGet("get")]
        public async Task<ActionResult> GetAllCompanies()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCompanyById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var company = await _companyService
                .GetCompanyByIdAsync(id, cancellationToken);

            if (company is null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany(
            [FromBody] CreateCompanyViewModel company,
            [FromServices] IValidator<CreateCompanyViewModel> validator,
            CancellationToken cancellationToken)
        {
            await ValidateAndChangeModelStateAsync(validator, company, cancellationToken);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCompany = await _companyService
                .CreateCompanyAsync(company, cancellationToken);

            return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
        }
    }
}