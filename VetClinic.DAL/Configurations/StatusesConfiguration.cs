using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class StatusesConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(t => t.StatusName)
                .HasConversion(new EnumToStringConverter<StatusName>())
                .HasMaxLength(20);

            builder.HasData(
                new Status[]
                {
                    new Status{ Id=1, StatusName=StatusName.Approved},
                    new Status{ Id=2, StatusName=StatusName.Disapproved},
                    new Status{ Id=3, StatusName=StatusName.Processing},
                    new Status{ Id=4, StatusName=StatusName.Completed},
                });
        }
    }
}
