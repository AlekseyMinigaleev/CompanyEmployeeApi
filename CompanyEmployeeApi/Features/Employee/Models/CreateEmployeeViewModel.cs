using CompanyEmployeeApi.Features.Compnay.Models;

namespace CompanyEmployeeApi.Features.Employee.Models
{
    public class BaseEmployeeViewModel(
     string firstName,
     string lastName,
     string position,
     string email)
    {
        public string FirstName { get; set; } = firstName;

        public string LastName { get; set; } = lastName;

        public string Position { get; set; } = position;

        public string Email { get; set; } = email;
    }

    public class CreateEmployeeViewModel(
        string firstName,
        string lastName,
        string position,
        string email) : BaseEmployeeViewModel(firstName, lastName, position, email)
    {
        public BaseCompanyViewModel Company { get; set; }
    }
}