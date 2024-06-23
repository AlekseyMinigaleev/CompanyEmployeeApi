using CompanyEmployeeApi.Features.Compnay.Models;
using CompanyEmployeeApi.Features.Employee.Models;
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
            var company = new CompanyModel(
               name: createCompanyVM.Name,
               address: createCompanyVM.Address,
               industry: createCompanyVM.Industry,
               employees: []);

            var employees = CreateEmployees(company, createCompanyVM.Employees);

            await SaveCompanyToDbAsync(company, employees, cancellationToken);

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

        private static List<EmployeeModel> CreateEmployees(
            CompanyModel company,
            BaseEmployeeViewModel[] employeeVMs)
        {
            var employees = new List<EmployeeModel>();

            foreach (var employeeVM in employeeVMs)
            {
                var employee = new EmployeeModel(
                    employeeVM.FirstName,
                    employeeVM.LastName,
                    employeeVM.Position,
                    company: company);

                employees.Add(employee);
            }

            return employees;
        }

        private async Task SaveCompanyToDbAsync(
            CompanyModel company,
            ICollection<EmployeeModel> employees,
            CancellationToken cancellationToken)
        {
            await _dbContext.Companies
            .AddAsync(company, cancellationToken);

            await _dbContext.Employees
                .AddRangeAsync(employees, cancellationToken);
            await _dbContext
                .SaveChangesAsync(cancellationToken);
        }
    }
}