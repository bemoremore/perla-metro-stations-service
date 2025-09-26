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
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. Add 'x-api-key: {key}'",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "x-api-key",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] { }
        }
    });
});

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

var extractedApiKey = Environment.GetEnvironmentVariable("API_KEY");

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();
    
    // Excluir rutas de Swagger del middleware de autenticación
    if (path != null && (path.StartsWith("/swagger") || path.Contains("/swagger.json")))
    {
        await next();
        return;
    }

    // Aplicar autenticación por API Key solo a los endpoints de la API
    if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKey) || apiKey != extractedApiKey)
    {

        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync($"Api Key is missing or invalid");
        return;
    }

    await next();
});
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StationsDbContext>();
    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(5);

    for (var i = 0; i < maxRetries; i++)
    {
        try
        {
            context.Database.Migrate();
            await DataSeeder.SeedData(context);
            break; // Exit the loop if migration is successful
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attempt {i + 1} of {maxRetries} failed: {ex.Message}");
            if (i == maxRetries - 1)
            {
                throw; // Rethrow the exception if it's the last attempt
            }
            await Task.Delay(delay);
        }
    }
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Perla Metro Stations API V1");
});
app.UseRouting();
app.UseAuthorization();
app.MapControllers();


app.Run();


