using DiagnosticsSummary.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DiagnosticsSummary.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            if (Database.IsRelational() && Database.GetMigrations().Any())
            {
                Database.Migrate();
            }
        }
        protected override void OnModelCreating(ModelBuilder m)
        {
            m.Entity<ChildDb>().Ignore(ch => ch.Keys);
            m.Entity<ChildDb>().Property(ch => ch.FIO).IsRequired();
            m.Entity<ChildDb>().Property(ch => ch.Gender).IsRequired();
            m.Entity<ChildDb>().Property(ch => ch.Age).IsRequired();
            m.Entity<ChildDb>().Property(ch => ch.AgeGroup).IsRequired();
            m.Entity<ChildDb>().Property(ch => ch.Group).IsRequired();
            m.Entity<ChildDb>().Property(ch => ch.Diagnosis).IsRequired();

            m.Entity<DiagnosticInfoDb>().Ignore(di => di.Keys);
            m.Entity<DiagnosticInfoDb>().HasKey(di => di.Name);
            m.Entity<DiagnosticInfoDb>().Property(di => di.ValueInterpreter).IsRequired();

            m.Entity<DiagnosticDb>().Ignore(e => e.Keys);
            m.Entity<DiagnosticDb>().Property(d => d.ChildId).IsRequired();
            m.Entity<DiagnosticDb>().Property(d => d.Name).IsRequired();
            m.Entity<DiagnosticDb>().Property(d => d.Value).IsRequired();
            m.Entity<DiagnosticDb>().Property(d => d.Year).IsRequired();
            m.Entity<DiagnosticDb>().Property(d => d.YearPart).IsRequired();
            base.OnModelCreating(m);
        }

        public DbSet<ChildDb> Children { get; set; }
        public DbSet<DiagnosticInfoDb> DiagnosticInfos { get; set; }
        public DbSet<DiagnosticDb> Diagnostics { get; set; }
    }
}
