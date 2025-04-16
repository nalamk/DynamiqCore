using DynamiqCore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DynamiqCore.Infrastructure.Persistence;

public class DynamiqCoreDbContext(DbContextOptions<DynamiqCoreDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    internal DbSet<Organization> Organizations { get; set; }
    internal DbSet<Patient> Patients { get; set; }
    internal DbSet<LookupConfiguration> LookupConfigurations { get; set; }

    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // PatientAddress is owned by Patient
        modelBuilder.Entity<Patient>()
            .OwnsOne(p => p.Address);
        
        // OrganizationAddress is owned by Organization
        modelBuilder.Entity<Organization>()
            .OwnsOne(p => p.Address);
        
        
        // Set default value for CreatedAt in Organization
        modelBuilder.Entity<Organization>()
            .Property(o => o.CreateAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        
        // Relationship between ApplicationUser and Organization
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Organization)
            .WithMany()
            .HasForeignKey(u => u.OrganizationId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // One-to-many relationship between Patient and Organization
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Organization)
            .WithMany(o => o.Patients)
            .HasForeignKey(p => p.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of organization if there are patients
        
        // Set default value for CreatedAt in Patient
        modelBuilder.Entity<Patient>()
            .Property(o => o.CreateAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

    }

    #endregion
}