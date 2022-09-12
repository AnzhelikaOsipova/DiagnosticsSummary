using DiagnosticsSummary.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DiagnosticsSummary.DataLayer
{
    public class DiagnosticContext: DbContext
    {
        public DiagnosticContext(DbContextOptions options) :
            base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ChildInfo> Children { get; set; }
        public DbSet<ChildDiagnostic> ChildDiagnostics { get; set; }
    }
}
