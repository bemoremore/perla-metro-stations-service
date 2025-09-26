using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace perla_metro_stations_service.src.DTOs
{
    /// <summary>
    /// Data Transfer Object for a station response.
    /// </summary>
    public class StationResponseDto
    {
        /// <summary>
        /// The unique identifier of the station.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the station.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The location of the station.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// The type of the station.
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Indicates whether the station is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The date and time when the station was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The date and time when the station was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}