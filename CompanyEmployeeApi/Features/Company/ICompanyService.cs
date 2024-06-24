using CompanyEmployeeApi.Features.Company.Models;
using DB.Models;

namespace CompanyEmployeeApi.Features.Company
{
    public interface ICompanyService
    {
        public Task<CompanyModel[]> GetAllAsync(
            CancellationToken cancellationToken);

        public Task<CompanyModel> CreateAsync(
            CreateCompanyViewModel createCompany,
            CancellationToken cancellationToken);

        public Task<CompanyModel?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<CompanyModel?> DeleteByIdsAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}