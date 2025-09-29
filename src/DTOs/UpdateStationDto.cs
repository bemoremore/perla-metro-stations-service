using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.DTOs
{
    /// <summary>
    /// Data Transfer Object for updating an existing station.
    /// </summary>
    public class UpdateStationDto
    {
        /// <summary>
        /// The name of the station.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The location of the station.
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// The type of the station.
        /// </summary>
        [EnumDataType(typeof(StationType), ErrorMessage = "Type must be 1 (Origen), 2 (Destino), or 3 (Intermedia).")]
        public StationType? Type { get; set; }
        /// <summary>
        /// Indicates whether the station is active.
        /// </summary>
        public bool? IsActive { get; set; }
    }
}