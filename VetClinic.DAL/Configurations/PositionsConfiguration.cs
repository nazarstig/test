using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class PositionsConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.Property(t => t.PositionName).HasMaxLength(40);

            builder.HasData(
                new Position[]
                {
                    new Position{ Id=1, PositionName="Вirector", Salary=100000},
                    new Position{ Id=2, PositionName="Сleaner", Salary=10000},
                    new Position{ Id=3, PositionName="Veterinarian", Salary=500},
                    new Position{ Id=4, PositionName="Stylist", Salary=1000}
                });
        }
    }
}
