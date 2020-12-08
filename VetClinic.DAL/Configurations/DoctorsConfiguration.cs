using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class DoctorsConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasMany(d => d.Appointments)
                .WithOne(d => d.Doctor)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(t => t.Education).HasMaxLength(100);
            builder.Property(t => t.Experience).HasMaxLength(200);
            
        }
    }
}
