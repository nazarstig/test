using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    class AppointmentProceduresConfiguration : IEntityTypeConfiguration<AppointmentProcedures>
    {
        public void Configure(EntityTypeBuilder<AppointmentProcedures> builder)
        {
            builder.HasData(
                 new AppointmentProcedures[]
                 {
                    new AppointmentProcedures{Id=1, AppointmentId=1, ProcedureId=1},
                    new AppointmentProcedures{Id=2, AppointmentId=2, ProcedureId=2},
                    new AppointmentProcedures{Id=3, AppointmentId=3, ProcedureId=3},
                    new AppointmentProcedures{Id=4, AppointmentId=4, ProcedureId=4},
                    new AppointmentProcedures{Id=5, AppointmentId=5, ProcedureId=5},
                    new AppointmentProcedures{Id=6, AppointmentId=6, ProcedureId=3},
                    new AppointmentProcedures{Id=7, AppointmentId=7, ProcedureId=5},
                    new AppointmentProcedures{Id=8, AppointmentId=7, ProcedureId=4},
                    new AppointmentProcedures{Id=9, AppointmentId=8, ProcedureId=4},
                    new AppointmentProcedures{Id=10, AppointmentId=1, ProcedureId=3}
                 });
        }
    }
}
