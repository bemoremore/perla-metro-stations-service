using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Helper
{
    /// <summary>
    /// Helper class for station type operations.
    /// </summary>
    public class GetType
    {
        /// <summary>
        /// Gets the display name for a station type.
        /// </summary>
        /// <param name="type">The station type.</param>
        /// <returns>The display name of the station type.</returns>
        public static string GetTypeDisplayName(StationType type)
        {
            return type switch
            {
                StationType.Origen => "Origen",
                StationType.Destino => "Destino",
                StationType.Intermedia => "Intermedia",
                _ => throw new ArgumentOutOfRangeException(nameof(type), "Invalid station type")
            };
        }
    }
}