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
            Name = name;
            Address = address;
            Industry = industry;
            Employees = employees;
        }

        private CompanyModel()
        { }


        /// <summary>
        /// Устанавливает список сотрудников компании и обновляет их принадлежность к текущей компании.
        /// </summary>
        /// <param name="employees">Список сотрудников компании.</param>
        public void SetEmployees(ICollection<EmployeeModel> employees) =>
            employees.ToList()
                .ForEach(x => x.SetCompany(this));

        /// <summary>
        /// Обновляет данные компании на основе переданных параметров.
        /// </summary>
        /// <param name="name">Новое название компании.</param>
        /// <param name="address">Новый адрес компании.</param>
        /// <param name="industry">Новая отрасль компании.</param>
        public void Update(
            string? name,
            string? address,
            string? industry) 
        {
            if (name is not null)
                Name = name;

            if (address is not null)
                Address = address;

            if (industry is not null)
                Industry = industry;
        }
    }
}