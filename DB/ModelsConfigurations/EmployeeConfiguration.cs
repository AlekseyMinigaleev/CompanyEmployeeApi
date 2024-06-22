using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.ModelsConfigurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.ToTable("Employee")
                .HasKey(x => x.Id);
        }
    }
}
