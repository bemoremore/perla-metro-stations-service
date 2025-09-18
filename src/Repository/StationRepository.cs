using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using perla_metro_stations_service.src.Interfaces;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.Repository
{
    public class StationRepository : IStationRepository
    {
        private readonly Data.StationsDbContext _context;

        public StationRepository(Data.StationsDbContext context)
        {
            _context = context;
        }


        public async Task<Station> CreateStationAsync(Station station)
        {
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();
            return station;
        }

        public async Task<bool> DeleteStationAsync(int id)
        {
            var station = await GetStationByIdAsync(id);
            if (station == null)
            {
                return false;
            }

            station.IsActive = false;
            station.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Station>> GetAllStationsAsync(bool includeInactive = false)
        {
            var query = _context.Stations.AsQueryable();
            if (!includeInactive)
            {
                query = query.Where(s => s.IsActive);
            }

            return await query
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<Station?> GetStationByIdAsync(int id)
        {
            return await _context.Stations.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> StationExistsAsync(int id)
        {
            return await _context.Stations.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> StationNameExistsAsync(string name, int? excludeId = null)
        {
            var query = _context.Stations.Where(s => s.Name.ToLower() == name.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(s => s.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<Station?> UpdateStationAsync(int id, Station station)
        {
            var existingStation = await GetStationByIdAsync(id);
            if (existingStation == null)
            {
                return null;
            }
            existingStation.Name = station.Name;
            existingStation.Location = station.Location;
            existingStation.Type = station.Type;
            existingStation.IsActive = station.IsActive;
            existingStation.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return existingStation;
        }
    }
}