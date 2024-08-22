using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sidio.MailBluster.Compliance;
using Sidio.MailBluster.Examples.MvcWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// In this example the api key is stored in the session. Therefor the options pattern is not used.
// This is not recommended for production use.
builder.Services.TryAddSingleton<IFlurlClientCache, FlurlClientCache>();

builder.Services
    .AddHttpContextAccessor()
    .AddScoped<MailBlusterService>()
    .AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = false;
});

builder.Services.AddLogging(
    x =>
    {
        x.EnableRedaction();
        x.ClearProviders();
        x.AddJsonConsole(option => option.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        });
        x.Services.AddRedaction(
            rb =>
            {
                rb.AddMailBlusterCompliance();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program;
