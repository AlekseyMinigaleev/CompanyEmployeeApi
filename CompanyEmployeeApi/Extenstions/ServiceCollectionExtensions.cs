using CompanyEmployeeApi.Features;
using CompanyEmployeeApi.Features.Company;
using CompanyEmployeeApi.Features.Employee;
using DB;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Extenstions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Метод расширения для добавления конфигурации хранилища данных в сервисы.
        /// </summary>
        /// <param name="services">Коллекция сервисов для добавления новых сервисов.</param>
        /// <param name="configuration">Конфигурация приложения для доступа к строке подключения.</param>
        public static void AddStorage(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration
                .GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Не удалось найти строку подключения к базе данных 'DefaultConnection'. Пожалуйста, укажите её в конфигурации вашего приложения.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)
            );
        }

        /// <summary>
        /// Метод расширения для добавления сервисов приложения в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов для добавления сервисов приложения.</param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICompanyCrudService, CompanyService>();
            services.AddTransient<IEmployeeCrudService, EmployeeService>();
        }
    }
}