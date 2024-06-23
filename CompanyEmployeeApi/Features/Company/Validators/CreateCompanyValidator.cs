using CompanyEmployeeApi.Features.Compnay.Models;
using CompanyEmployeeApi.Features.Employee.Validators;
using DB;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Company.Validators
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyViewModel>
    {
        public CreateCompanyValidator(
            AppDbContext dbContext)
        {
            Include(new BaseCompanyValidator(dbContext));
            RuleForEach(x => x.Employees).SetValidator(new BaseEmployeeValidator(dbContext));
        }
    }

    public class BaseCompanyValidator : AbstractValidator<BaseCompanyViewModel>
    {
        public BaseCompanyValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithErrorCode(nameof(BaseCompanyViewModel.Name))
                    .WithMessage($"{nameof(BaseCompanyViewModel.Name)} is required.")
                 .Must(name =>
                 {
                     var company = dbContext.Companies
                        .SingleOrDefault(x => x.Name.Equals(name));

                     return company is null;
                 })
                    .WithErrorCode(nameof(BaseCompanyViewModel.Name))
                    .WithMessage($"{nameof(BaseCompanyViewModel.Name)} is unique.");

            RuleFor(x => x.Address)
                .NotEmpty()
                    .WithErrorCode(nameof(BaseCompanyViewModel.Address))
                    .WithMessage($"{nameof(BaseCompanyViewModel.Address)} is required.");

            RuleFor(x => x.Industry)
                .NotEmpty()
                    .WithErrorCode(nameof(BaseCompanyViewModel.Industry))
                    .WithMessage($"{nameof(BaseCompanyViewModel.Industry)} is required.");
        }
    }
}