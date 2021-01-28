using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VetClinic.DAL.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(32);
            builder.Property(t => t.NormalizedName).HasMaxLength(32);

            builder.HasData(
                new IdentityRole[]
                {
                    new IdentityRole{ Id = "542636ae-953e-4e04-a7de-7533f25af173", Name="client", NormalizedName = "CLIENT"},
                    new IdentityRole{ Id = "e65dd8e1-34f8-4e9d-b535-3b03dde2500e", Name="doctor", NormalizedName = "DOCTOR"},
                    new IdentityRole{ Id = "9149c77e-5c15-416a-9bed-e361330feb92", Name="admin", NormalizedName = "ADMIN"},
                    new IdentityRole{ Id = "ced370e3-1401-4190-9960-ab5bf41f162e", Name="accountant", NormalizedName = "ACCOUNTANT"},
                });
        }
    }
}