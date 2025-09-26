using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Data
{
    public class DataSeeder
    {
        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task SeedData(StationsDbContext context)
        {
            if (!context.Stations.Any())
            {
                var stations = new List<Station>
                {
                    new Station
                    {
                        Name = "Estación Central",
                        Location = "Av. Principal 100, Centro, Antofagasta",
                        Type = StationType.Origen.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Station
                    {
                        Name = "Plaza de Armas",
                        Location = "Plaza de Armas s/n, Centro, Antofagasta",
                        Type = StationType.Intermedia.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Station
                    {
                        Name = "Universidad Católica del Norte",
                        Location = "Av. Angamos 0610, Antofagasta",
                        Type = StationType.Intermedia.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Station
                    {
                        Name = "Hospital Regional",
                        Location = "Av. Argentina 1962, Antofagasta",
                        Type = StationType.Intermedia.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Station
                    {
                        Name = "Mall Plaza Antofagasta",
                        Location = "Av. Balmaceda 2355, Antofagasta",
                        Type = StationType.Intermedia.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Station
                    {
                        Name = "Terminal Norte",
                        Location = "Av. Grecia 1000, Antofagasta",
                        Type = StationType.Destino.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Station
                    {
                        Name = "Puerto de Antofagasta",
                        Location = "Terminal Portuario, Antofagasta",
                        Type = StationType.Intermedia.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },
                    new Station
                    {
                        Name = "Estadio Regional",
                        Location = "Av. Stadium s/n, Antofagasta",
                        Type = StationType.Intermedia.ToString(),
                        IsActive = false, // Estación en mantenimiento
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await context.Stations.AddRangeAsync(stations);
                await context.SaveChangesAsync();
            }
        }
    }
}