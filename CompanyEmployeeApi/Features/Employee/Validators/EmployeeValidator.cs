using CompanyEmployeeApi.Features.Employee.Models;
using DB;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Employee.Validators
{
    public class EmployeeValidator :AbstractValidator<EmployeeViewModel>
    {
        public EmployeeValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                 .NotEmpty()
                     .WithErrorCode(nameof(CreateEmployeeViewModel.FirstName))
                     .WithMessage($"{nameof(CreateEmployeeViewModel.FirstName)} is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                    .WithErrorCode(nameof(CreateEmployeeViewModel.LastName))
                    .WithMessage($"{nameof(CreateEmployeeViewModel.LastName)} is required.");

            RuleFor(x => x.Position)
                .NotEmpty()
                    .WithErrorCode(nameof(CreateEmployeeViewModel.Position))
                    .WithMessage($"{nameof(CreateEmployeeViewModel.Position)} is required.");

            RuleFor(x => x.Email)
                .EmailAddress()
                    .WithErrorCode(nameof(CreateEmployeeViewModel.Email))
                    .WithMessage($"{nameof(CreateEmployeeViewModel.Email)} is Email.");

            RuleFor(x => x.CompanyId)
                .Must(companyId =>
                {
                    if (companyId is null)
                        return true;

                    var employee = dbContext.Companies.SingleOrDefault(x => x.Id == companyId);

                    return employee is not null;
                });
        }
    }
}
