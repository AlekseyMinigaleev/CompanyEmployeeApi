using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.ModelsConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<CompanyModel>
    {
        public void Configure(EntityTypeBuilder<CompanyModel> builder)
        {
            builder.ToTable("Company")
                .HasKey(x => x.Id);

            builder.HasMany(x => x.Employees)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}