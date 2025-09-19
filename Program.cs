using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using perla_metro_stations_service.src.Data;
using perla_metro_stations_service.src.Interfaces;
using perla_metro_stations_service.src.Repository;
using perla_metro_stations_service.src.Services;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StationsDbContext>(options =>
{
var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "perla_metro_stations_dev";
var user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password";
var connectionString = $"Server={host};Port={port};Database={dbName};User={user};Password={password};";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
options.UseMySql(connectionString, serverVersion, options =>
    {
        options.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
    Console.WriteLine($"Using connection string: {connectionString} and server version: {serverVersion}");

}
);
// Repositories
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IStationService, StationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StationsDbContext>();
    context.Database.Migrate();
    await DataSeeder.SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
//app.UseHttpsRedirection();


app.Run();


