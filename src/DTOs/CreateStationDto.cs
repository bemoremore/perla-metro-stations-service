using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace perla_metro_stations_service.src.DTOs
{
    /// <summary>
    /// Data Transfer Object for creating a new station.
    /// </summary>
    public class CreateStationDto
    {
        /// <summary>
        /// The name of the station.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The location of the station.
        /// </summary>
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// The type of the station.
        /// </summary>
        [Required(ErrorMessage = "Type is required.")]
        /// <summary>
        /// The type of the station.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}