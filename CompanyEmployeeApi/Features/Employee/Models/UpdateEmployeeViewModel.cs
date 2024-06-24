using System.Text.Json.Serialization;

namespace CompanyEmployeeApi.Features.Employee.Models
{
    public class UpdateEmployeeViewModel (
        string firstName,
        string lastName,
        string position,
        string email,
        Guid? companyId,
        Guid employeeId) : EmployeeViewModel(firstName, lastName, position, email, companyId)
    {
        [JsonIgnore]
        public Guid EmployeeId { get; set; } = employeeId;
    }
}
