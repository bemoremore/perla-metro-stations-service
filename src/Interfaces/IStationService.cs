using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.DTOs;

namespace perla_metro_stations_service.src.Interfaces
{
    /// <summary>
    /// Service interface for managing stations.
    /// </summary>
    public interface IStationService
    {
        /// <summary>
        /// Gets all stations.
        /// </summary>
        /// <returns>A list of all stations.</returns>
        Task<IEnumerable<StationResponseDto>> GetAllStationsAsync();
        /// <summary>
        /// Gets a station by its ID.
        /// </summary>
        /// <param name="id">The ID of the station.</param>
        /// <returns>The station if found; otherwise, null.</returns>
        Task<StationResponseDto?> GetStationByIdAsync(int id);
        /// <summary>
        /// Creates a new station.
        /// </summary>
        /// <param name="createDto">The station data to create.</param>
        /// <returns>The created station as DTO.</returns>
        Task<StationResponseDto> CreateStationAsync(CreateStationDto createDto);
        /// <summary>
        /// Updates an existing station.
        /// </summary>
        /// <param name="id">The ID of the station to update.</param>
        /// <param name="updateDto">The updated station data.</param>
        /// <returns>The updated station if successful; otherwise, null.</returns>
        Task<StationResponseDto?> UpdateStationAsync(int id, UpdateStationDto updateDto);
        /// <summary>
        /// Deletes a station by its ID.
        /// </summary>
        /// <param name="id">The ID of the station to delete.</param>
        /// <returns>True if the station was deleted; otherwise, false.</returns>
        Task<bool> DeleteStationAsync(int id);
    }
}