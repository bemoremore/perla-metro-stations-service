using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.DTOs;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Mappers
{
    /// <summary>
    /// Mapper class for converting between Station models and DTOs.
    /// </summary>
    public static class StationMapper
    {
        /// <summary>
        /// Converts a Station model to a StationResponseDto.
        /// </summary>
        /// <param name="station">The Station model to convert.</param>
        /// <returns>The corresponding StationResponseDto.</returns>
        public static StationResponseDto ToStationResponse(this Station station)
        {
            return new StationResponseDto
            {
                Id = station.Id,
                Name = station.Name,
                Location = station.Location,
                Type = station.Type,
                IsActive = station.IsActive,
                CreatedAt = station.CreatedAt,
                UpdatedAt = station.UpdatedAt
            };
        }
        /// <summary>
        /// Converts a CreateStationDto to a Station model.
        /// </summary>
        /// <param name="createStationDto">The CreateStationDto to convert.</param>
        /// <returns>The corresponding Station model.</returns>
        public static Station ToStation(this CreateStationDto createStationDto)
        {
            return new Station
            {
                Name = createStationDto.Name,
                Location = createStationDto.Location,
                Type = Helper.GetType.GetTypeDisplayName(createStationDto.Type),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}