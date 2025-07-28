using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace WebApplicationTest.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RiskTheme> RiskTheme { get; set; }
        public DbSet<RiskSubThemeLevel1> RiskSubThemeLevel1 { get; set; }
        public DbSet<RiskSubThemeLevel2> RiskSubThemeLevel2 { get; set; }
        public DbSet<RiskSubThemeLevel3> RiskSubThemeLevel3 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping for relationships
            modelBuilder.Entity<RiskSubThemeLevel1>()
                .HasOne(l1 => l1.RiskTheme)
                .WithMany()
                .HasForeignKey(l1 => l1.RiskThemeId);

            modelBuilder.Entity<RiskSubThemeLevel2>()
                .HasOne(l2 => l2.RiskTheme)
                .WithMany()
                .HasForeignKey(l2 => l2.RiskThemeId);

            modelBuilder.Entity<RiskSubThemeLevel2>()
                .HasOne(l2 => l2.RiskSubThemeLevel1)
                .WithMany()
                .HasForeignKey(l2 => l2.RiskSubThemeLevel1Id);

            modelBuilder.Entity<RiskSubThemeLevel3>()
                .HasOne(l3 => l3.RiskTheme)
                .WithMany()
                .HasForeignKey(l3 => l3.RiskThemeId);

            modelBuilder.Entity<RiskSubThemeLevel3>()
                .HasOne(l3 => l3.RiskSubThemeLevel2)
                .WithMany()
                .HasForeignKey(l3 => l3.RiskSubThemeLevel2Id);
        }
    }
}

