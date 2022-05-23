using Weather.Plugins.EFCore;
using Weather.Plugins.EFCore.Plugins;
using Weather.BusinessLogic.Interfaces;
using Weather.BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WeatherContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Weather App")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<WeatherContext>();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IWeatherReportRepository, WeatherReportRepository>();
builder.Services.AddTransient<IViewWeatherReports, WeatherReportService>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var weatherContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
weatherContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
