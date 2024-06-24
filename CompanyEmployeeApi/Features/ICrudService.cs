using CompanyEmployeeApi.Features.Company.Models;
using CompanyEmployeeApi.Features.Employee.Models;
using DB.Models;

namespace CompanyEmployeeApi.Features
{
    /// <summary>
    /// Интерфейс, представляющий основные CRUD операции для модели.
    /// </summary>
    /// <typeparam name="TModel">Тип модели.</typeparam>
    /// <typeparam name="TCreateViewModel">ViewModel для создания.</typeparam>
    /// <typeparam name="TUpdateViewModel">ViewModel для обновления.</typeparam>
    public interface ICrudService<TModel, TCreateViewModel, TUpdateViewModel>
    {
        /// <summary>
        /// Получает все записи модели.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Массив всех записей модели.</returns>
        Task<TModel[]> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Получает запись модели по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Запись модели или null, если запись не найдена.</returns>
        Task<TModel?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Создает новую запись модели.
        /// </summary>
        /// <param name="createViewModel">ViewModel для создания новой записи.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданная запись модели.</returns>
        Task<TModel> CreateAsync(
            TCreateViewModel createViewModel,
            CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет запись модели по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой записи.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Удаленная запись модели или null, если запись не найдена.</returns>
        Task<TModel?> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет запись модели по идентификатору.
        /// </summary>
        /// <param name="updateViewModel">ViewModel для обновления записи.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Обновленная запись модели или null, если запись не найдена.</returns>
        Task<TModel?> UpdateByIdAsync(
            TUpdateViewModel updateViewModel,
            CancellationToken cancellationToken);
    }


    /// <summary>
    /// Интерфейс, представляющий CRUD операции для сотрудников.
    /// </summary>
    public interface IEmployeeCrudService : ICrudService<EmployeeModel, CreateEmployeeViewModel, UpdateEmployeeViewModel>
    { }

    /// <summary>
    /// Интерфейс, представляющий CRUD операции для компаний.
    /// </summary>
    public interface ICompanyCrudService : ICrudService<CompanyModel, CreateCompanyViewModel, UpdateCompanyViewModel>
    { }
}

