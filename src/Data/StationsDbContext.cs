using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Data
{
    public class StationsDbContext : DbContext
    {
        public StationsDbContext(DbContextOptions<StationsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }

        
    }
}