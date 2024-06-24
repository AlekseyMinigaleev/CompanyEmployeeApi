namespace CompanyEmployeeApi.Features.Company.Models
{
    public class CreateCompanyViewModel(
        string name,
        string address,
        string industry,
        Guid[] employeeIds) : CompanyViewModel(name, address, industry, employeeIds)
    { }
}