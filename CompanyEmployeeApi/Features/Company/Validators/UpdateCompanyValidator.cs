using CompanyEmployeeApi.Features.Company.Models;
using DB;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Company.Validators
{
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyViewModel>
    {
        public UpdateCompanyValidator(AppDbContext dbContext)
        {
            Include(new CompanyValidator(dbContext));

            RuleFor(x => x)
              .Must(viewModel =>
              {
                  var updatedCompany = dbContext.Companies
                    .Single(x => x.Id == viewModel.CompanyId);

                  if (updatedCompany.Name.Equals(viewModel.Name))
                  {
                      return true;
                  }
                  else
                  {
                      var companyWithSameName = dbContext.Companies
                        .SingleOrDefault(x => x.Name.Equals(viewModel.Name));

                      return companyWithSameName is null;
                  }
              })
              .WithErrorCode(nameof(CreateCompanyViewModel.Name))
              .WithMessage($"{nameof(CreateCompanyViewModel.Name)} is unique.");
        }
    }
}