using CompanyEmployeeApi.Features.Employee.Models;
using DB;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Employee.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeViewModel>
    {
        public CreateEmployeeValidator(AppDbContext dbContext)
        {
            Include(new EmployeeValidator(dbContext));

            RuleFor(x => x.Email)
                .Must((email) =>
                {
                    var employee = dbContext.Employees
                        .SingleOrDefault(x => x.Email == email);

                    return employee is null;
                })
                .WithErrorCode(nameof(CreateEmployeeViewModel.Email))
                .WithMessage($"{nameof(CreateEmployeeViewModel.Email)} is unique.");
        }
    }
}