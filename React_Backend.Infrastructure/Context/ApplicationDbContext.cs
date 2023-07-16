using React_Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace React_Backend.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
            .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(100);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
            this.SeedDoctorSchedule(builder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>();
            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>();

        }

        private void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser doctor = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "doctor",
                NormalizedUserName="DOCTOR",
                FirstName = "doctor",
                LastName = "doctor",
                Email = "doctor@gmail.com",
                NormalizedEmail="DOCTOR@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };
            var docPassword = new PasswordHasher<ApplicationUser>();
            var hashed = docPassword.HashPassword(doctor, "Doctor*123");
            doctor.PasswordHash = hashed;

            ApplicationUser patient = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e6",
                FirstName = "patient",
                LastName = "patient",
                UserName = "patient",
                NormalizedUserName="PATIENT",
                Email = "patient@gmail.com",
                NormalizedEmail="PATIENT@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
                
            };

            var patPassword = new PasswordHasher<ApplicationUser>();
            var hashed1 = patPassword.HashPassword(patient, "Patient*123");
            patient.PasswordHash = hashed1;
            builder.Entity<ApplicationUser>().HasData(doctor);
            builder.Entity<ApplicationUser>().HasData(patient);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Doctor", ConcurrencyStamp = "1", NormalizedName = "Doctor" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "Patient", ConcurrencyStamp = "2", NormalizedName = "Patient" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330", UserId = "b74ddd14-6340-4840-95c2-db12554843e6" }
                );
        }

        private void SeedDoctorSchedule(ModelBuilder builder)
        {
            builder.Entity<Schedule>().HasData(
                new Schedule() { 
                    Id =1,
                    Day="Monday",
                    DoctorId= "b74ddd14-6340-4840-95c2-db12554843e5",
                    StartTime= TimeOnly.Parse("09:00"),
                    EndTime = TimeOnly.Parse("12:00"),
                    Status=true
                }
              );
            builder.Entity<Schedule>().HasData(
                new Schedule()
                {
                    Id =2,
                    Day = "TuseDay",
                    DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
                    StartTime = TimeOnly.Parse("09:00"),
                    EndTime = TimeOnly.Parse("12:00"),
                    Status = true
                }
              );
            builder.Entity<Schedule>().HasData(
               new Schedule()
               {
                   Id = 3,
                   Day = "WednesDay",
                   DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
                   StartTime = TimeOnly.Parse("09:00"),
                   EndTime = TimeOnly.Parse("12:00"),
                   Status = true
               }
             );
        }
    }
}
