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

            modelBuilder.Entity<AppRole>(entity =>
            {
                entity
                .HasKey(u => u.Id);
                entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AppRoleClaim>(entity =>
            {
                entity
                .HasKey(u => u.Id);
                entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity
                .HasKey(u => u.Id);
                entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AppUserClaim>(entity =>
            {
                entity
                .HasKey(u => u.Id);
                entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AppUserLogin>(entity =>
            {
                entity
                .HasKey(u => u.Id);
                entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AppUserRole>(entity =>
            {
                entity
                .HasKey(u => new { u.RoleId, u.UserId });
            });

            modelBuilder.Entity<AppUserToken>(entity =>
            {
                entity
                .HasKey(u => u.Id);
                entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            });

        }
    }
}