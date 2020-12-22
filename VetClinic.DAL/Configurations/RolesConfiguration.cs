using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetClinic.DAL.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(32);
            builder.Property(t => t.NormalizedName).HasMaxLength(32);

            builder.HasData(
                new IdentityRole[]
                {
                    new IdentityRole{ Name="client", NormalizedName = "CLIENT"},
                    new IdentityRole{ Name="doctor", NormalizedName = "DOCTOR"},
                    new IdentityRole{ Name="admin", NormalizedName = "ADMIN"},
                    new IdentityRole{ Name="accountant", NormalizedName = "ACCOUNTANT"},
                });
        }
    }
}