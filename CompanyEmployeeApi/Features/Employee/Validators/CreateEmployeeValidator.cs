using CompanyEmployeeApi.Features.Company.Validators;
using CompanyEmployeeApi.Features.Employee.Models;
using DB;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Employee.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeViewModel>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Company)
                .SetValidator(new BaseCompanyValidator());
        }
    }

    public class BaseEmployeeValidator : AbstractValidator<BaseEmployeeViewModel>
    {
        public BaseEmployeeValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                    .WithErrorCode(nameof(BaseEmployeeViewModel.FirstName))
                    .WithMessage($"{nameof(BaseEmployeeViewModel.FirstName)} is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                    .WithErrorCode(nameof(BaseEmployeeViewModel.LastName))
                    .WithMessage($"{nameof(BaseEmployeeViewModel.LastName)} is required.");

            RuleFor(x => x.Position)
                .NotEmpty()
                    .WithErrorCode(nameof(BaseEmployeeViewModel.Position))
                    .WithMessage($"{nameof(BaseEmployeeViewModel.Position)} is required.");

            RuleFor(x => x.Email)
                .Must((email) =>
                {
                    var employee = dbContext.Employees
                        .SingleOrDefault(x => x.Email == email);

                    return employee is not null;
                })
                    .WithErrorCode(nameof(BaseEmployeeViewModel.Email))
                    .WithMessage($"{nameof(BaseEmployeeViewModel.Email)} is unique.");
        }
    }
}