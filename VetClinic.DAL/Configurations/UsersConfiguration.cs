using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {            
            builder.Property(t => t.Email).HasMaxLength(50);
            builder.Property(t => t.NormalizedEmail).HasMaxLength(50);
            builder.Property(t => t.PasswordHash).HasMaxLength(30);
            builder.Property(t => t.PhoneNumber).HasMaxLength(12);            
            builder.Property(t => t.UserName).HasMaxLength(50);
        }
    }
}
