using CompanyEmployeeApi.Features.Employee.Models;
using DB;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Employee.Validators
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeViewModel>
    {
        public UpdateEmployeeValidator(AppDbContext dbContext)
        {
            Include(new EmployeeValidator(dbContext));

            RuleFor(x => x)
               .Must((viewModel) =>
               {
                   var updatedEmployee = dbContext.Employees
                    .Single(x => x.Id == viewModel.EmployeeId);

                   if (updatedEmployee.Email.Equals(viewModel.Email))
                   {
                       return true;
                   }
                   else
                   {
                       var companyWithSameName = dbContext.Employees
                         .SingleOrDefault(x => x.Email.Equals(viewModel.Email));

                       return companyWithSameName is null;
                   }
               })
               .WithErrorCode(nameof(CreateEmployeeViewModel.Email))
               .WithMessage($"{nameof(CreateEmployeeViewModel.Email)} is unique.");
        }
    }
}