using DiagnosticsSummary.Api.Models;
using DiagnosticsSummary.DAL;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DiagnosticsSummary.Services.Models;
using DiagnosticsSummary.Services.Contracts;

namespace DiagnosticsSummary.Api
{
    public static class DependencyInjection
    {
        public static void SetupServerDI(this IServiceCollection services, Settings settings)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File("api.log")
                .CreateLogger();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<DataContext>(
               isp => new DataContext(
                   new DbContextOptionsBuilder<DataContext>()
                       .UseSqlite(settings.ConnectionString, 
                            b => b.MigrationsAssembly("DiagnosticsSummary.Api"))
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                       .Options));
            services.AddScoped<DbContext, DataContext>(sp => sp.GetService<DataContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IChildService, ChildService>();
            services.AddScoped<IDiagnosticInfoService, DiagnosticInfoService>();
            services.AddScoped<IDiagnosticService, DiagnosticService>();
        }
    }
}
