using DB;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployeeApi.Extenstions
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Выполняет все необходимые процессы, для корректной работы приложения
        /// </summary>
        /// <remarks>
        /// Подразумевает в момент выполнения наличие базы данных для Hangfire
        /// </remarks>
        public static async Task InitApplicationAsync(
            this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var (logger, dbContext) = GetDependencies(scope);

            logger.LogInformation("Начало выполнения миграций:");
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Все миграции успешно выполнены");
        }

        private static (ILogger , AppDbContext) GetDependencies(IServiceScope scope)
        {
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<Program>>();

            var dbContext = services.GetRequiredService<AppDbContext>();

            return (logger, dbContext);
        }
    }
}
