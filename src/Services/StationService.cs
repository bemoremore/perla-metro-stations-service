using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.DTOs;
using perla_metro_stations_service.src.Interfaces;
using perla_metro_stations_service.src.Mappers;

namespace perla_metro_stations_service.src.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _stationRepository;
        public StationService(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<StationResponseDto> CreateStationAsync(CreateStationDto createDto)
        {
            var station = createDto.ToStation();
            var createdStation = await _stationRepository.CreateStationAsync(station);
            return createdStation.ToStationResponse();
        }

        public async Task<bool> DeleteStationAsync(int id)
        {
            return await _stationRepository.DeleteStationAsync(id);
        }

        public async Task<IEnumerable<StationResponseDto>> GetAllStationsAsync()
        {
            var stations = await _stationRepository.GetAllStationsAsync();
            return stations.Select(s => s.ToStationResponse());
        }
        

        public async Task<StationResponseDto?> GetStationByIdAsync(int id)
        {
            var station = await _stationRepository.GetStationByIdAsync(id);
            return station?.ToStationResponse();
        }

        public async Task<StationResponseDto?> UpdateStationAsync(int id, UpdateStationDto updateDto)
        {
            var existingStation = await _stationRepository.GetStationByIdAsync(id);
            if (existingStation == null)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(updateDto.Name))
            {
                existingStation.Name = updateDto.Name;
            }
            if (!string.IsNullOrWhiteSpace(updateDto.Location))
            {
                existingStation.Location = updateDto.Location;
            }
            if (updateDto.Type.HasValue)
            {
                existingStation.Type = updateDto.Type.Value.ToString();
            }
            if (updateDto.IsActive.HasValue)
            {
                existingStation.IsActive = updateDto.IsActive.Value;
            }
            existingStation.UpdatedAt = DateTime.UtcNow;
            var updatedStation = await _stationRepository.UpdateStationAsync(id, existingStation);
            return updatedStation?.ToStationResponse();
            
        }
    }
}