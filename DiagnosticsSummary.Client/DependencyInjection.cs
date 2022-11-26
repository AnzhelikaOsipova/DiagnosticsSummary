using Serilog;

namespace DiagnosticsSummary.Client
{
    public static class DependencyInjection
    {
        public static void SetupClientDI(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File("app.log")
                .CreateLogger();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
