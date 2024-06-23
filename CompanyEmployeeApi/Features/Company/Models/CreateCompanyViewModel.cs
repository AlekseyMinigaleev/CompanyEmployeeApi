using CompanyEmployeeApi.Features.Employee.Models;

namespace CompanyEmployeeApi.Features.Compnay.Models
{
    public class CreateCompanyViewModel(
        string name,
        string address,
        string industry,
        BaseEmployeeViewModel[] employees) 
            : BaseCompanyViewModel(name, address, industry)
    {
        public BaseEmployeeViewModel[] Employees { get; set; } = employees;
    }

    public class BaseCompanyViewModel(
        string name,
        string address,
        string industry)
    {
        public string Name { get; set; } = name;

        public string Address { get; set; } = address;

        public string Industry { get; set; } = industry;
    }
}