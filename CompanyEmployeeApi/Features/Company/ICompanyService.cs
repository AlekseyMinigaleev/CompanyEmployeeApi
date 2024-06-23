using CompanyEmployeeApi.Features.Compnay.Models;
using DB.Models;

namespace CompanyEmployeeApi.Features.Company
{
    public interface ICompanyService
    {
        Task<CompanyModel> CreateCompanyAsync(CreateCompanyViewModel createCompany, CancellationToken cancellationToken);

        Task<CompanyModel?> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
