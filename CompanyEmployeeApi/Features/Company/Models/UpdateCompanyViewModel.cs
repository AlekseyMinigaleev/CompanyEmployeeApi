using System.Text.Json.Serialization;

namespace CompanyEmployeeApi.Features.Company.Models
{
    public class UpdateCompanyViewModel(
        string name,
        string address,
        string industry,
        Guid[] employeeIds,
        Guid companyId) : CompanyViewModel(name, address, industry, employeeIds)
    {
        [JsonIgnore]
        public Guid CompanyId { get; set; } = companyId;
    }
}
