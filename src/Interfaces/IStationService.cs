using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.DTOs;

namespace perla_metro_stations_service.src.Interfaces
{
    public interface IStationService
    {
        Task<IEnumerable<StationResponseDto>> GetAllStationsAsync();
        Task<StationResponseDto?> GetStationByIdAsync(int id);
        Task<StationResponseDto> CreateStationAsync(CreateStationDto createDto);
        Task<StationResponseDto?> UpdateStationAsync(int id, UpdateStationDto updateDto);
        Task<bool> DeleteStationAsync(int id);
    }
}