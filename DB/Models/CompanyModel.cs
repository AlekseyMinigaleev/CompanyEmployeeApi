namespace DB.Models
{
    /// <summary>
    /// Модель компании.
    /// </summary>
    public class CompanyModel
    {
        /// <summary>
        /// Уникальный идентификатор компании.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Название компании.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Адрес компании.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Отрасль компании.
        /// </summary>
        public string Industry { get; private set; }

        /// <summary>
        /// Список сотрудников компании.
        /// </summary>
        public ICollection<EmployeeModel> Employees { get; private set; }

        public CompanyModel(
            string name,
            string address,
            string industry,
            ICollection<EmployeeModel> employees)
        {
            Id = Guid.NewGuid();
            PopulateCompanyInfo(
                name: name,
                address: address,
                industry: industry,
                employees: employees);
        }

        private CompanyModel()
        { }

        /// <summary>
        /// Обновляет данные компании на основе переданных параметров.
        /// </summary>
        /// <param name="name">Новое название компании.</param>
        /// <param name="address">Новый адрес компании.</param>
        /// <param name="industry">Новая отрасль компании.</param>
        public void Update(
            string name,
            string address,
            string industry,
            ICollection<EmployeeModel> employees)
        {
            PopulateCompanyInfo(
                name: name,
                address: address,
                industry: industry,
                employees: employees);

            Employees
                .Where(emp => !employees.Any(e => e.Id == emp.Id))
                .ToList()
                .ForEach(x=>x.SetCompany(null));
        }

        /// <summary>
        /// Устанавливает список сотрудников компании и обновляет их принадлежность к текущей компании.
        /// </summary>
        /// <param name="employees">Список сотрудников компании.</param>
        public void AddEmployees(ICollection<EmployeeModel> employees) =>
                employees.ToList()
                    .ForEach(x => x.SetCompany(this));

        private void PopulateCompanyInfo(
            string name,
            string address,
            string industry,
            ICollection<EmployeeModel> employees)
        {
            Name = name;
            Address = address;
            Industry = industry;

            AddEmployees(employees);
        }
    }
}