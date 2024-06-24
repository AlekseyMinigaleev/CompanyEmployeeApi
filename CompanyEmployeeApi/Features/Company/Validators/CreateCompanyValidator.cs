using CompanyEmployeeApi.Features.Company.Models;
using DB;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Company.Validators
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyViewModel>
    {
        public CreateCompanyValidator(
            AppDbContext dbContext)
        {
            Include(new CompanyValidator(dbContext));

            RuleFor(x => x.Name)
                .Must(name =>
                {
                    var company = dbContext.Companies
                        .SingleOrDefault(x => x.Name.Equals(name));

                    return company is null;
                })
                .WithErrorCode(nameof(CreateCompanyViewModel.Name))
                .WithMessage($"{nameof(CreateCompanyViewModel.Name)} is unique.");

            RuleFor(x => x.EmployeeIds)
                .Must(ids =>
                {
                    var existEmployeeIds = dbContext.Employees
                        .Where(x => x.CompanyId == null)
                        .ToArray();

                    var isExistedIds = existEmployeeIds.Length == ids.Length;

                    return isExistedIds;
                })
                .WithErrorCode(nameof(CreateCompanyViewModel.EmployeeIds))
                .WithMessage($"Each employee must not belong to any company.");
        }
    }
}