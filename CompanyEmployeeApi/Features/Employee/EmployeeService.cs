using CompanyEmployeeApi.Features.Employee.Models;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Features.Employee
{
    public class EmployeeService(AppDbContext dbContext) : IEmployeeService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<EmployeeModel[]> GetAllAsync(CancellationToken cancellationToken) =>
            await _dbContext.Employees
                .Include(x => x.Company)
                .ToArrayAsync(cancellationToken);

        public async Task<EmployeeModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _dbContext.Employees
                .Include(x => x.Company)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<EmployeeModel> CreateAsync(
            CreateEmployeeViewModel createEmployeeVM,
            CancellationToken cancellationToken)
        {
            var company = await _dbContext.Companies
                .SingleOrDefaultAsync(
                    x => x.Id == createEmployeeVM.CompanyId,
                    cancellationToken);

            var employee = new EmployeeModel(
                firstName: createEmployeeVM.FirstName,
                lastName: createEmployeeVM.LastName,
                position: createEmployeeVM.Position,
                email: createEmployeeVM.Email,
                company: company);

            await _dbContext.Employees.AddAsync(employee, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return employee;
        }

        public async Task<EmployeeModel?> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var employeeToDelete = await _dbContext.Employees
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (employeeToDelete is not null)
            {
                _dbContext.Employees.Remove(employeeToDelete);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return employeeToDelete;
        }

        public async Task<EmployeeModel?> UpdateByIdAsync(
            UpdateEmployeeViewModel updateEmployeeVm,
            CancellationToken cancellationToken)
        {
            var companyToUpdate = await _dbContext.Employees
                .Include(x => x.Company)
                .SingleOrDefaultAsync(
                    x => x.Id == updateEmployeeVm.EmployeeId,
                    cancellationToken);

            if (companyToUpdate is not null)
            {
                var company = await _dbContext.Companies
                    .SingleOrDefaultAsync(
                        x => x.Id == updateEmployeeVm.CompanyId,
                        cancellationToken);

                companyToUpdate.Update(
                    firstName: updateEmployeeVm.FirstName,
                    lastName: updateEmployeeVm.LastName,
                    position: updateEmployeeVm.Position,
                    email: updateEmployeeVm.Email,
                    company: company);

                await _dbContext
                    .SaveChangesAsync(cancellationToken);
            }

            return companyToUpdate;
        }
    }
}