using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Interfaces
{
    /// <summary>
    /// Repository interface for managing stations.
    /// </summary>
    public interface IStationRepository
    {
        /// <summary>
        /// Gets all stations.
        /// </summary>
        /// <param name="includeInactive">Include the inactive stations.</param>
        /// <returns>All the stations.</returns>
        Task<IEnumerable<Station>> GetAllStationsAsync(bool includeInactive = false);
        /// <summary>
        /// Gets a station by its ID.
        /// </summary>
        /// <param name="id">ID of the station.</param>
        /// <returns>The station if found; otherwise, null.</returns>
        Task<Station?> GetStationByIdAsync(int id);
        /// <summary>
        /// Creates a new station.
        /// </summary>
        /// <param name="station">The station to create.</param>
        /// <returns>The created station.</returns>
        Task<Station> CreateStationAsync(Station station);
        /// <summary>
        /// Updates an existing station.
        /// </summary>
        /// <param name="station">The station to update.</param>
        /// <param name="id">The ID of the station to update.</param>
        /// <returns>The updated station if successful; otherwise, null.</returns>
        Task<Station?> UpdateStationAsync(int id, Station station);
        /// <summary>
        /// Deletes a station by its ID.
        /// </summary>
        /// <param name="id">The ID of the station to delete.</param>
        /// <returns>True if the station was deleted; otherwise, false.</returns>
        Task<bool> DeleteStationAsync(int id);
        /// <summary>
        /// Checks if a station exists by its ID.
        /// </summary>
        /// <param name="id">The ID of the station to check.</param>
        /// <returns>True if the station exists; otherwise, false.</returns>
        Task<bool> StationExistsAsync(int id);
        /// <summary>
        /// Checks if a station exists by its name.
        /// </summary>
        /// <param name="name">The name of the station to check.</param>
        /// <param name="excludeId">An optional ID to exclude from the check.</param>
        /// <returns>True if the station exists; otherwise, false.</returns>
        Task<bool> StationNameExistsAsync(string name, int? excludeId = null);
    }
}