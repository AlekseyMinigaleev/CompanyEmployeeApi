namespace DB.Models
{
    /// <summary>
    /// Модель сотрудника.
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Уникальный идентификатор сотрудника.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Фамилия сотрудника.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Должность сотрудника.
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        /// Электронная почта сотрудника.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Дата начала работы сотрудника в компании.
        /// </summary>
        public DateTime? EmploymentDate { get; private set; }

        /// <summary>
        /// Дата увольнения сотрудника.
        /// </summary>
        public DateTime? TerminationDate { get; private set; }

        /// <summary>
        /// Уникальный идентификатор компании, к которой принадлежит сотрудник.
        /// </summary>
        public Guid? CompanyId { get; private set; }

        /// <summary>
        /// Компания, к которой принадлежит сотрудник.
        /// </summary>
        public CompanyModel? Company { get; private set; }

        public EmployeeModel(
            string firstName,
            string lastName,
            string position,
            string email,
            CompanyModel? company)
        {
            Id = Guid.NewGuid();

            Update(
                firstName: firstName,
                lastName: lastName,
                position: position,
                email: email,
                company: company);
        }

        private EmployeeModel()
        { }

        /// <summary>
        /// Устанавливает компанию, к которой принадлежит сотрудник.
        /// Если <paramref name="company"/> равно <see langword="null"/>, сотрудник увольняется из текущей компании.
        /// </summary>
        /// <param name="company">Компания, к которой принадлежит сотрудник.</param>
        public void SetCompany(CompanyModel? company)
        {

            if (company is null && Company is not null)
            {
                TerminationDate = DateTime.UtcNow;
            }
            else if (company is null)
            {
                EmploymentDate = null;
            }
            else
            {
                EmploymentDate = DateTime.UtcNow;
                TerminationDate = null;
            }

            Company = company;
            CompanyId = company?.Id;
        }

        /// <summary>
        /// Обновляет информацию о сотруднике на основе переданных параметров.
        /// </summary>
        /// <param name="firstName">Новое имя сотрудника.</param>
        /// <param name="lastName">Новая фамилия сотрудника.</param>
        /// <param name="position">Новая должность сотрудника.</param>
        public void Update(
            string firstName,
            string lastName,
            string position,
            string email,
            CompanyModel? company)
        {
                FirstName = firstName;
                LastName = lastName;
            Position = position;
            Email = email;

            SetCompany(company);
        }
    }
}