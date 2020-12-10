using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class ProceduresConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.Property(t => t.ProcedureName).HasMaxLength(100);

            builder.HasData(
                new Procedure[]
                {
                    new Procedure{Id=1, IsSelectable=true, ProcedureName="SPA procedures", Description="Best for your pet", Price=1000},
                    new Procedure{Id=2, IsSelectable=false, ProcedureName="Operation", Description="Paw fracture", Price=2000},
                    new Procedure{Id=3, IsSelectable=true, ProcedureName="Examination of animal", Description="Pet inspection", Price=50},
                    new Procedure{Id=4, IsSelectable=false, ProcedureName="Сastration", Description="Bad operation", Price=240},
                    new Procedure{Id=5, IsSelectable=false, ProcedureName="Consultation", Description="Doctor Consultation about care and maintenance of the animal", Price=100},                   
                });
        }
    }
}
