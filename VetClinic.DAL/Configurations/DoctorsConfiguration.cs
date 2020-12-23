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
            builder.HasOne(c => c.Position)
                .WithMany(c => c.Doctors)
                .HasForeignKey(c => c.PositionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.Education).HasMaxLength(100);
            builder.Property(t => t.Experience).HasMaxLength(200);
            builder.HasData(
                new Doctor[]
                {
                    new Doctor{ Id=1, UserId="1", Education="Gas and Oil", Experience="2", PositionId=1},
                    new Doctor{ Id=2, UserId="2", Education="IfMedical", Experience="7", PositionId=3},
                    new Doctor{ Id=3, UserId="3", Education="SelfEducation", Experience="3", PositionId=4},

                });
        }
    }
}
