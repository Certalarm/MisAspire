using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MisAspire.Domain.Entity;
using static MisAspire.Persistence.Extensions.MisContextExtensions;

namespace MisAspire.Persistence.Context
{
    public class MisContext: DbContext
    {
        protected IConfiguration _configuration;

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors =>  Set<Doctor>();
        public DbSet<Disease> Diseases => Set<Disease>();
        public DbSet<PatientDisease> PatientDiseases => Set<PatientDisease>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        #region .ctors
        public MisContext(DbContextOptions<MisContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
                .UseSeeding((context, _) =>
                    ((MisContext)context).SeedStartData());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AssignRelationsPatientDisease(modelBuilder);
            AssignRelationsAppointment(modelBuilder);
        }

        private static void AssignRelationsPatientDisease(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientDisease>()
                .HasKey(pd => pd.Id );

            modelBuilder.Entity<PatientDisease>()
                .HasOne(pd => pd.Patient)
                .WithMany(p => p.PatientDiseases)
                .HasForeignKey(pd => pd.PatientId);

            modelBuilder.Entity<PatientDisease>()
                .HasOne(pd => pd.Disease)
                .WithMany(d => d.PatientDiseases)
                .HasForeignKey(pd => pd.DiseaseId);
        }

        private static void AssignRelationsAppointment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Appointment>()
                            .HasOne(a => a.Patient)
                            .WithMany(p => p.Appointments)
                            .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);
        }
    }
}
