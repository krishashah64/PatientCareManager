using Microsoft.EntityFrameworkCore;
using Backend;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientRecommendation> PatientRecommendations { get; set; }
        public DbSet<PatientFile> PatientFiles { get; set; }
        public DbSet<PatientMedicalHistory> PatientMedicalHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasMany(p => p.Recommendations)
            .WithOne(r => r.Patient)
            .HasForeignKey(r => r.PatientId);
    }
    }
}
