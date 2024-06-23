using CompanyEmployeeApi.Features.Compnay.Models;

namespace CompanyEmployeeApi.Features.Employee.Models
{
    public class BaseEmployeeViewModel(
     string firstName,
     string lastName,
     string position)
    {
        public string FirstName { get; set; } = firstName;

        public string LastName { get; set; } = lastName;

        public string Position { get; set; } = position;
    }

    public class CreateEmployeeViewModel(
        string firstName,
        string lastName,
        string position) : BaseEmployeeViewModel(firstName, lastName, position)
    {
        public BaseCompanyViewModel Company { get; set; }
    }
}