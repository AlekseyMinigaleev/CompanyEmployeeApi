using CompanyEmployeeApi.Features.Compnay.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployeeApi.Features.Company
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController(ICompanyService companyService) : ControllerBase
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
            CancellationToken cancellationToken)
        {
            var createdCompany = await _companyService
                .CreateCompanyAsync(company, cancellationToken);

            return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
        }
    }
}