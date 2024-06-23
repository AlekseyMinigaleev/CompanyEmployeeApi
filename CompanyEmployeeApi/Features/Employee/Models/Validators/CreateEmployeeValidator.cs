using CompanyEmployeeApi.Features.Company.Validators;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Employee.Models.Validators
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
        public BaseEmployeeValidator()
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
        }
    }
}