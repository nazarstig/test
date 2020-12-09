using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class AnimalTypesConfiguration : IEntityTypeConfiguration<AnimalType>
    {
        public void Configure(EntityTypeBuilder<AnimalType> builder)
        {
            builder.Property(t => t.AnimalTypeName).HasMaxLength(30);

            builder.HasData(
                new AnimalType[]
                {
                    new AnimalType{ Id=1,AnimalTypeName="Pes dvorovuy"},
                    new AnimalType{ Id=2,AnimalTypeName="Kit Domashniy"},
                    new AnimalType{ Id=3,AnimalTypeName="Slon"},
                    new AnimalType{ Id=4,AnimalTypeName="Zhuraph"},
                    new AnimalType{ Id=5,AnimalTypeName="Zolota Rubka"},
                    new AnimalType{ Id=6,AnimalTypeName="Homyak"},
                    new AnimalType{ Id=7,AnimalTypeName="Monster"}
                });

        }
    }
}
