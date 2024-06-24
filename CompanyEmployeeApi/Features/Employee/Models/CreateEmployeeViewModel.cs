namespace CompanyEmployeeApi.Features.Employee.Models
{
    public class CreateEmployeeViewModel(
        string firstName,
        string lastName,
        string position,
        string email,
        Guid? companyId) : EmployeeViewModel(firstName, lastName, position, email, companyId)
    {

    }
}