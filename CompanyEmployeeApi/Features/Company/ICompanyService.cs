using CompanyEmployeeApi.Features.Company.Models;
using DB.Models;

namespace CompanyEmployeeApi.Features.Company
{
    public interface ICompanyService
    {
        public Task<CompanyModel> CreateCompanyAsync(
            CreateCompanyViewModel createCompany,
            CancellationToken cancellationToken);

        public Task<CompanyModel?> GetCompanyByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<CompanyModel[]> GetAllCompaniesAsync(
            CancellationToken cancellationToken);
    }
}