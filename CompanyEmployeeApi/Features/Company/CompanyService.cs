using CompanyEmployeeApi.Features.Company.Models;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Features.Company
{
    public class CompanyService(AppDbContext dbContext) : ICompanyService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<CompanyModel> CreateCompanyAsync(
            CreateCompanyViewModel createCompanyVM,
            CancellationToken cancellationToken)
        {
            var employees = await _dbContext.Employees
                .Where(x => createCompanyVM.EmployeeIds.Contains(x.Id))
                .ToArrayAsync(cancellationToken);

            var company = new CompanyModel(
               name: createCompanyVM.Name,
               address: createCompanyVM.Address,
               industry: createCompanyVM.Industry,
               employees: employees);

            await _dbContext.Companies
                .AddAsync(company, cancellationToken);
            await _dbContext
              .SaveChangesAsync(cancellationToken);

            return company;
        }

        public async Task<CompanyModel[]> GetAllCompaniesAsync(CancellationToken cancellationToken) =>
            await _dbContext.Companies
                .Include(x => x.Employees)
                .ToArrayAsync(cancellationToken);

        public async Task<CompanyModel?> GetCompanyByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var company = await _dbContext.Companies
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return company;
        }
    }
}