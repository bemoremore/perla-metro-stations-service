using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace perla_metro_stations_service.src.Models
{
    /// <summary>
    /// Represents a metro station.
    /// </summary>
    public class Station
    {
        /// <summary>
        /// The unique identifier for the station.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the station.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The location of the station.
        /// </summary>
        [Required]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// The type of the station.
        /// </summary>
        [Required]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Indicates whether the station is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// The date and time when the station was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// The date and time when the station was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

    }
    /// <summary>
    /// Enumeration for station types.
    /// </summary>
    public enum StationType
    {
        Origen = 1,
        Destino = 2,
        Intermedia = 3
    }
}