﻿using DiagnosticsSummary.Api.DAL;
using DiagnosticsSummary.Api.Services;
using DiagnosticsSummary.DAL;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace DiagnosticsSummary.Api
{
    public static class DI
    {
        public static void Setup(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File("api.log")
                .CreateLogger();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<DataContext>(
               isp => new DataContext(
                   new DbContextOptionsBuilder<DataContext>()
                       .UseSqlite("Data Source=DiagnosticsSummary.db", 
                            b => b.MigrationsAssembly("DiagnosticsSummary.Api"))
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                       .Options));
            services.AddScoped<DbContext, DataContext>(sp => sp.GetService<DataContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ChildService>();
            services.AddScoped<DiagnosticInfoService>();
            services.AddScoped<DiagnosticService>();
        }
    }
}
