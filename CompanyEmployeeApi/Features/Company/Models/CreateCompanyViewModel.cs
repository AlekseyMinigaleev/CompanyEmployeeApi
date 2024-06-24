namespace CompanyEmployeeApi.Features.Company.Models
{
    public class CreateCompanyViewModel(
        string name,
        string address,
        string industry,
        Guid[] employeeIds)
    {
        public string Name { get; set; } = name;

        public string Address { get; set; } = address;

        public string Industry { get; set; } = industry;

        public Guid[] EmployeeIds { get; set; } = employeeIds;
    }
}