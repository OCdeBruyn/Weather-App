using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weather.Plugins.EFCore;
using Weather.Plugins.EFCore.Plugins;
using Weather.BusinessLogic.Interfaces;
using Weather.BusinessLogic.Services;

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var scope = app.Services.CreateScope();
var weatherContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
weatherContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
