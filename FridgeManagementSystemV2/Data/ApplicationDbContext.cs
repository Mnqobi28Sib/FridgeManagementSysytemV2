using FridgeManagementSystemV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FridgeManagementSystemV2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<MaintenanceVisit> MaintenanceVisits { get; set; }
        public DbSet<ServicePerformed> ServicesPerformed { get; set; }
        public DbSet<FaultReport> FaultReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaintenanceVisit>()
                .HasOne(m => m.Technician)
                .WithMany()
                .HasForeignKey(m => m.TechnicianId);

            modelBuilder.Entity<MaintenanceVisit>()
                .HasOne(m => m.Fridge)
                .WithMany()
                .HasForeignKey(m => m.FridgeId);

            modelBuilder.Entity<Fridge>()
                .HasMany(f => f.FaultReports)
                .WithOne(r => r.Fridge)
                .HasForeignKey(r => r.FridgeId);
        }
    }
}
