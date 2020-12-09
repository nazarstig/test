using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class AnimalsConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(20);
            builder.Property(t => t.Photo).HasMaxLength(150);

            builder.HasData(
                new Animal[]
                {
                   new Animal{ Id=1, Name="Pushok", Age=342, ClientId=3,AnimalTypeId=7},
                   new Animal{ Id=2, Name="Ruzhuk", Age=3, ClientId=1,AnimalTypeId=2},
                   new Animal{ Id=3, Name="Sirko", Age=5, ClientId=2,AnimalTypeId=1},
                   new Animal{ Id=4, Name="Biznes", Age=12, ClientId=4,AnimalTypeId=3},
                   new Animal{ Id=5, Name="Hmarochos", Age=4, ClientId=4,AnimalTypeId=4},
                   new Animal{ Id=6, Name="Krug", Age=1, ClientId=1,AnimalTypeId=6},
                   new Animal{ Id=7, Name="Dzvin", Age=1, ClientId=3,AnimalTypeId=5},
                   new Animal{ Id=8, Name="Robin", Age=2, ClientId=1,AnimalTypeId=2}
                });
        }
    }
}
