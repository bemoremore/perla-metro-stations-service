using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.DTOs;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Mappers
{
    public static class StationMapper
    {
        public static StationResponseDto ToStationRespose(this Station station)
        {
            return new StationResponseDto
            {
                Id = station.Id,
                Name = station.Name,
                Location = station.Location,
                Type = station.Type.ToString(),
                IsActive = station.IsActive,
                CreatedAt = station.CreatedAt,
                UpdatedAt = station.UpdatedAt
            };
        }

        public static Station ToStation(this CreateStationDto createStationDto)
        {
            return new Station
            {
                Name = createStationDto.Name,
                Location = createStationDto.Location,
                Type = createStationDto.Type,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}