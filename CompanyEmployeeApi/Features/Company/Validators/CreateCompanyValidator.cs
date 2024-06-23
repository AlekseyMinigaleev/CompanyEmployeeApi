﻿using CompanyEmployeeApi.Features.Compnay.Models;
using CompanyEmployeeApi.Features.Employee.Models.Validators;
using FluentValidation;

namespace CompanyEmployeeApi.Features.Company.Validators
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyViewModel>
    {
        public CreateCompanyValidator()
        {
            RuleForEach(x => x.Employees).SetValidator(new BaseEmployeeValidator());
        }
    }

    public class BaseCompanyValidator : AbstractValidator<BaseCompanyViewModel>
    {
        public BaseCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithErrorCode(nameof(BaseCompanyViewModel.Name))
                    .WithMessage($"{nameof(BaseCompanyViewModel.Name)} is required.");

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