namespace DB.Models
{
    /// <summary>
    /// Модель компании.
    /// </summary>
    public class CompanyModel(
        string name,
        string address,
        string industry,
        ICollection<EmployeeModel> employees)
    {
        /// <summary>
        /// Уникальный идентификатор компании.
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// Название компании.
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// Адрес компании.
        /// </summary>
        public string Address { get; private set; } = address;

        /// <summary>
        /// Отрасль компании.
        /// </summary>
        public string Industry { get; private set; } = industry;

        /// <summary>
        /// Список сотрудников компании.
        /// </summary>
        public ICollection<EmployeeModel> Employees { get; private set; } = employees;

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