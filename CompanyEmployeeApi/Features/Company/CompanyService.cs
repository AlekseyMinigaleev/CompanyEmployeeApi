﻿using CompanyEmployeeApi.Features.Company.Models;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Features.Company
{
    public class CompanyService(AppDbContext dbContext) : ICompanyService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<CompanyModel> CreateAsync(
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

        public async Task<CompanyModel[]> GetAllAsync(CancellationToken cancellationToken) =>
            await _dbContext.Companies
                .Include(x => x.Employees)
                .ToArrayAsync(cancellationToken);

        public async Task<CompanyModel?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var company = await _dbContext.Companies
                .Include(x => x.Employees)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return company;
        }

        public async Task<CompanyModel?> DeleteByIdsAsync(Guid id, CancellationToken cancellationToken)
        {
            var companyToDelete = await _dbContext.Companies
                .Include(x => x.Employees)
                .SingleOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);

            if (companyToDelete is not null)
            {
                companyToDelete.Employees
                    .ToList()
                    .ForEach(x => x.SetCompany(null));

                _dbContext.Companies.Remove(companyToDelete);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return companyToDelete;
        }

        public async Task<CompanyModel?> UpdateByIdAsync(
            Guid id,
            UpdateCompanyViewModel companyVM,
            CancellationToken cancellationToken)
        {
            var companyToUpdate = await _dbContext.Companies
                .Include(x => x.Employees)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (companyToUpdate is not null)
            {
                var employees = await _dbContext.Employees
                    .Where(x => companyVM.EmployeeIds.Contains(x.Id))
                    .ToArrayAsync(cancellationToken);

                companyToUpdate.Update(
                    name: companyVM.Name,
                    address: companyVM.Address,
                    industry: companyVM.Industry,
                    employees: employees
                    );

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return companyToUpdate;
        }
    }
}