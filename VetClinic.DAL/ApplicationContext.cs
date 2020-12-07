using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL
{
    public class ApplicationContext : IdentityDbContext
    {
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalType> AnimalTypes { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Procedure> Procedures { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(d => d.Doctor)
                .OnDelete(DeleteBehavior.NoAction);

            
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = "memberId",
                        Name = "adminId"
                    },
                    new IdentityRole
                    {
                        Id = "adminId",
                        Name = "admin"
                    }
                );

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "aliceId",
                        UserName = "alice",
                        Email = "AliceSmith@email.com",
                        EmailConfirmed = true,
                        PasswordHash = "Pass123$",
                    },
                    new User
                    {
                        Id = "bobId",
                        UserName = "bob",
                        Email = "BobSmith@email.com",
                        EmailConfirmed = true,
                        PasswordHash = "Pass123$",
                    }
                );

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>()
                    {
                        RoleId = "adminId",
                        UserId = "aliceId"
                    },

                    new IdentityUserRole<string>()
                    {
                        RoleId = "memberId",
                        UserId = "bobId"
                    }
                );
            
        }
    }
}