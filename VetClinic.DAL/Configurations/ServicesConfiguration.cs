using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(t => t.ServiceName).HasMaxLength(100);
        }
    }
}
