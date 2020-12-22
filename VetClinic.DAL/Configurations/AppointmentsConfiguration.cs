using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Configurations
{
    public class AppointmentsConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            
            builder
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);
            
            builder
                .HasOne(a => a.Animal)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.AnimalId);
            
            builder
                .HasOne(a => a.Status)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StatusId);
            builder
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId);
            builder
                .HasMany(a => a.AppointmentProcedures)
                .WithOne(a => a.Appointment)
                .HasForeignKey(a => a.AppointmentId);

            builder.Property(a => a.AppointmentDate).IsRequired();
            builder.Property(a => a.Complaints).IsRequired().HasMaxLength(100);
            builder.Property(a => a.TreatmentDescription).IsRequired(false).HasMaxLength(500);

            builder.Property(a => a.DoctorId).IsRequired(false);
            builder.Property(a => a.AnimalId).IsRequired();
            builder.Property(a => a.StatusId).IsRequired().HasDefaultValue(3);
            builder.Property(a => a.ServiceId).IsRequired();


            builder.HasData(
                new Appointment
                {
                    Id = 1, AnimalId = 1, AppointmentDate = new DateTime(2020, 5, 1, 8, 30, 0),
                    Complaints = "Temperature", TreatmentDescription = "Drink more wather", DoctorId = 1, ServiceId = 1,
                    StatusId = 2
                },
                new Appointment
                {
                    Id = 2, AnimalId = 2, AppointmentDate = new DateTime(2020, 4, 23, 9, 30, 0),
                    Complaints = "Bad wool", TreatmentDescription = "Use animal shampoo", DoctorId = 3, ServiceId = 2,
                    StatusId = 3
                },
                new Appointment
                {
                    Id = 3, AnimalId = 3, AppointmentDate = new DateTime(2020, 1, 12, 10, 30, 0),
                    Complaints = "Required castration", TreatmentDescription = "wear a collar for 12 days", DoctorId = 2,
                    ServiceId = 4, StatusId = 4
                },
                new Appointment
                {
                    Id = 4, AnimalId = 4, AppointmentDate = new DateTime(2019, 5, 14, 11, 30, 0),
                    Complaints = "Spa procedure", TreatmentDescription = "Spa every weak", DoctorId = 3, ServiceId = 2,
                    StatusId = 1
                },
                new Appointment
                {
                    Id = 5, AnimalId = 5, AppointmentDate = new DateTime(2019, 11, 15, 12, 30, 0),
                    Complaints = "Need consultation", TreatmentDescription = "Pay more attention", DoctorId = 3,
                    ServiceId = 1, StatusId = 4
                },
                new Appointment
                {
                    Id = 6, AnimalId = 6, AppointmentDate = new DateTime(2019, 2, 2, 9, 30, 0),
                    Complaints = "Vaccination", TreatmentDescription = "", DoctorId = 2, ServiceId = 1, StatusId = 3
                },
                new Appointment
                {
                    Id = 7, AnimalId = 7, AppointmentDate = new DateTime(2020, 12, 12, 12, 30, 0),
                    Complaints = "Sore spine", TreatmentDescription = "daily walk", DoctorId = 2, ServiceId = 1, StatusId = 2
                },
                new Appointment
                {
                    Id = 8, AnimalId = 8, AppointmentDate = new DateTime(2020, 3, 30, 10, 30, 0),
                    Complaints = "Paw fracture", TreatmentDescription = "daily bandage change", DoctorId = 2, ServiceId = 3,
                    StatusId = 1
                });
        }
    }
}