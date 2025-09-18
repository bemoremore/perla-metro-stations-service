using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Data
{
    public class DataSeeder
    {
        public static void SeedData(StationsDbContext context)
        {
            if (!context.Stations.Any())
            {
                var stations = new List<Station>
                {
                    new Station
                    {
                        Name = "Central Station",
                        Location = "Downtown",
                        Type = StationType.Origen.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Station
                    {
                        Name = "City Park Station",
                        Location = "City Park",
                        Type = StationType.Intermedia.ToString(),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Station
                    {
                        Name = "Airport Station",
                        Location = "Airport",
                        Type = StationType.Destino.ToString(),
                        IsActive = false,
                        CreatedAt = DateTime.UtcNow,
                    }
                };

                context.Stations.AddRange(stations);
                context.SaveChanges();
            }
        }
    }
}