using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Data
{
    /// <summary>
    /// Entity Framework Core database context for station data.
    /// </summary>
    public class StationsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the StationsDbContext class.
        /// </summary>
        /// <param name="options"></param>
        public StationsDbContext(DbContextOptions<StationsDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// The Stations DbSet.
        /// </summary>
        public DbSet<Station> Stations { get; set; }


    }
}