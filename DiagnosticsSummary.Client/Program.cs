using DiagnosticsSummary.DataLayer;
using DiagnosticsSummary.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddLogging(log => log.AddProvider(new DebugLoggerProvider()));
builder.Services.AddSingleton<ILogger>(sp => sp.GetService<ILoggerFactory>().CreateLogger("DLog"));
builder.Services.AddTransient<DbContextOptions>(sp => new DbContextOptionsBuilder()
                                                .UseSqlite("Data Source=DiagnosticsSummary.db").Options);
builder.Services.AddSingleton<IDataAccess, DataAccess>();
builder.Services.AddTransient<ChildService>();
builder.Services.AddTransient<DiagnosticsService>();
builder.Services.AddTransient<DiagnosticsSummaryManager>();

var app = builder.Build();

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("Приложение откроется по ссылке: https://localhost:5001");
Console.ResetColor();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
