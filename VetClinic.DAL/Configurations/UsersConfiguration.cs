using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.FirstName).HasMaxLength(30);
            builder.Property(t => t.LastName).HasMaxLength(30);
            builder.Property(t => t.Email).HasMaxLength(50);
            builder.Property(t => t.NormalizedEmail).HasMaxLength(50);
            builder.Property(t => t.PasswordHash).HasMaxLength(300);
            builder.Property(t => t.PhoneNumber).HasMaxLength(12);            
            builder.Property(t => t.UserName).HasMaxLength(50);
            

            builder.HasData(
                new User[]
                {
                    new User{ Id="1", UserName="Koleso Anastasiya", Email="KolesoAnastasiya@gmail.com", PhoneNumber="0984112333"},
                    new User{ Id="2", UserName="Nazarenko Oleh", Email="NazarenkoOleh@gmail.com", PhoneNumber="0954453374"},
                    new User{ Id="3", UserName="Noorkova Shuba", Email="NoorkovaShuba@gmail.com", PhoneNumber="0934453214"},
                    new User{ Id="4", UserName="Vozniy Andriy", Email="VozniyAndriy@gmail.com", PhoneNumber="0931412622"},
                    new User{ Id="5", UserName="Kosovich Maruna", Email="KosovichMaruna@gmail.com", PhoneNumber="0681236324"},
                    new User{ Id="6", UserName="Wernudub Ivan", Email="WernudubIvan@gmail.com", PhoneNumber="0982123654"},
                    new User{ Id="7", UserName="Mukolenko Nadiya", Email="MukolenkoNadiya@gmail.com", PhoneNumber="0982131254"},
                });
            
        }
    }
}
