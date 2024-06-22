using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<CompanyModel> Companies { get; set; }

        public DbSet<EmployeeModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}