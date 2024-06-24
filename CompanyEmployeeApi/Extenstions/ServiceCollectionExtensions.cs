using CompanyEmployeeApi.Features;
using CompanyEmployeeApi.Features.Company;
using CompanyEmployeeApi.Features.Employee;
using DB;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Extenstions
{
    public static class ServiceCollectionExtensions
    {
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

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}