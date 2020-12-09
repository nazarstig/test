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

            builder.HasData(
                new Service[]
                {
                    new Service{ Id=1, ServiceName="Inspection"},
                    new Service{ Id=2, ServiceName="Makeup"},
                    new Service{ Id=3, ServiceName="Urgently"},
                    new Service{ Id=4, ServiceName="Castration"},
                });
        }
    }
}
