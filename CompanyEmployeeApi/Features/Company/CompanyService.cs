using CompanyEmployeeApi.Features.Compnay.Models;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Features.Company
{
    public class CompanyService(AppDbContext dbContext) : ICompanyService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<CompanyModel> CreateCompanyAsync(
            CreateCompanyViewModel createCompany,
            CancellationToken cancellationToken)
        {
            var company = new CompanyModel(
                name: createCompany.Name,
                address: createCompany.Address,
                industry: createCompany.Industry,
                employees: []);

            await _dbContext.Companies
               .AddAsync(company, cancellationToken);

            var employees = new List<EmployeeModel>();
            foreach (var employeeViewModel in createCompany.Employees)
            {
                var employee = new EmployeeModel(
                    employeeViewModel.FirstName,
                    employeeViewModel.LastName,
                    employeeViewModel.Position,
                    company: company);

                employees.Add(employee);
            }
            await _dbContext.Employees
                .AddRangeAsync(employees, cancellationToken);

            await _dbContext
                .SaveChangesAsync(cancellationToken);

            return company;
        }

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