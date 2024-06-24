using CompanyEmployeeApi.Features.Company.Models;
using DB;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Features.Company.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyViewModel>
    {
        public CompanyValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithErrorCode(nameof(CreateCompanyViewModel.Name))
                    .WithMessage($"{nameof(CreateCompanyViewModel.Name)} is required.");

            RuleFor(x => x.Address)
                .NotEmpty()
                    .WithErrorCode(nameof(CreateCompanyViewModel.Address))
                    .WithMessage($"{nameof(CreateCompanyViewModel.Address)} is required.");

            RuleFor(x => x.Industry)
                .NotEmpty()
                    .WithErrorCode(nameof(CreateCompanyViewModel.Industry))
                    .WithMessage($"{nameof(CreateCompanyViewModel.Industry)} is required.");

            RuleFor(x => x.EmployeeIds)
               .Must(ids =>
               {
                   if (ids is null || ids.Length == 0)
                       return true;

                   var isIdsUnique = ids.Distinct().Count() == ids.Length;

                   if (!isIdsUnique)
                       return false;

                   var existEmployeeIds = dbContext.Employees
                       .Where(x => ids.Contains(x.Id))
                       .ToArray();

                   var isExistedIds = existEmployeeIds.Length == ids.Length;

                   return isExistedIds;
               })
               .WithErrorCode(nameof(CreateCompanyViewModel.EmployeeIds))
               .WithMessage($"EmployeeIds must be unique and exist in the database.");
        }
    }
}
