using CompanyEmployeeApi.Features.Company.Models;
using CompanyEmployeeApi.Features.Employee.Models;
using DB.Models;

namespace CompanyEmployeeApi.Features
{
    public interface ICrudService<TModel, TCreateViewModel, TUpdateViewModel>
    {
        Task<TModel[]> GetAllAsync(
            CancellationToken cancellationToken);

        Task<TModel?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<TModel> CreateAsync(
            TCreateViewModel createViewModel,
            CancellationToken cancellationToken);

        Task<TModel?> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<TModel?> UpdateByIdAsync(
            TUpdateViewModel updateViewModel,
            CancellationToken cancellationToken);
    }

    public interface IEmployeeService : ICrudService<EmployeeModel, CreateEmployeeViewModel, UpdateEmployeeViewModel>
    { }

    public interface ICompanyService : ICrudService<CompanyModel, CreateCompanyViewModel, UpdateCompanyViewModel>
    { }
}

