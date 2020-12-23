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
                    new User{ Id="1", FirstName = "Anastasiya", LastName = "Koleso", UserName="Koleso Anastasiya", Email="KolesoAnastasiya@gmail.com", PhoneNumber="0984112333"},
                    new User{ Id="2", FirstName = "Oleh", LastName = "Nazarenko", UserName="Nazarenko Oleh", Email="NazarenkoOleh@gmail.com", PhoneNumber="0954453374"},
                    new User{ Id="3", FirstName = "Shuba", LastName = "Noorkova", UserName="Noorkova Shuba", Email="NoorkovaShuba@gmail.com", PhoneNumber="0934453214"},
                    new User{ Id="4", FirstName = "Andriy", LastName = "Vozniy", UserName="Vozniy Andriy", Email="VozniyAndriy@gmail.com", PhoneNumber="0931412622"},
                    new User{ Id="5", FirstName = "Maruna", LastName = "Kosovich", UserName="Kosovich Maruna", Email="KosovichMaruna@gmail.com", PhoneNumber="0681236324"},
                    new User{ Id="6", FirstName = "Ivan", LastName = "Wernudub", UserName="Wernudub Ivan", Email="WernudubIvan@gmail.com", PhoneNumber="0982123654"},
                    new User{ Id="7", FirstName = "Nadiya", LastName = "Mukolenko", UserName="Mukolenko Nadiya", Email="MukolenkoNadiya@gmail.com", PhoneNumber="0982131254"},
                });
            
        }
    }
}
