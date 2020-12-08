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
                
        }
    }
}
