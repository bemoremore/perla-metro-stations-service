using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using perla_metro_stations_service.src.DTOs;
using perla_metro_stations_service.src.Interfaces;

namespace perla_metro_stations_service.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;
        private readonly ILogger<StationsController> _logger;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<StationsController>();
        }
        /// <summary>
        /// Gets all stations.
        /// <returns>List of stations.</returns>
        /// <response code="200">Stations retrieved successfully.</response>
        /// <response code="500">Internal server error.</response>
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationResponseDto>>> GetAllStations()
        {
            try
            {
                var stations = await _stationService.GetAllStationsAsync();
                return Ok(new { success = true, data = stations });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing your request.");
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }
        /// <summary>
        /// Gets a station by its ID.
        /// <param name="id">ID of the station.</param>
        /// <returns>The station if found; otherwise, 404 Not Found.</returns>
        /// <response code="200">Station found successfully.</response>
        /// <response code="404">Station not found.</response>
        /// <response code="500">Internal server error.</response>
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StationResponseDto>> GetStationById(Guid id)
        {
            try
            {
                var station = await _stationService.GetStationByIdAsync(id);
                if (station == null)
                {
                    return NotFound(new { success = false, message = "Station not found." });
                }
                return Ok(new { success = true, data = station });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing your request.");
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Creates a new station.
        /// <param name="createDto">The station to create.</param>
        /// <returns>The created station.</returns>
        /// <response code="201">Station created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="409">Station with the same name already exists.</response>
        /// <response code="500">Internal server error.</response>
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<StationResponseDto>> CreateStation([FromBody] CreateStationDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid input data.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
                }

                var existingStation = await _stationService.GetAllStationsAsync();
                if (existingStation.Any(s => s.Name.Equals(createDto.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    return Conflict(new { success = false, message = "A station with the same name already exists." });
                }

                var createdStation = await _stationService.CreateStationAsync(createDto);
                return CreatedAtAction(nameof(GetStationById), new { id = createdStation.Id }, new { success = true, data = createdStation });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing your request.");
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Updates an existing station.
        /// <param name="id">ID of the station to update.</param>
        /// <param name="updateDto">The station data to update.</param>
        /// <returns>The updated station if successful; otherwise, 404 Not Found.</returns>
        /// <response code="200">Station updated successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="404">Station not found.</response>
        /// <response code="409">Station with the same name already exists.</response>
        /// <response code="500">Internal server error.</response>
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<StationResponseDto>> UpdateStation(Guid id, [FromBody] UpdateStationDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid input data.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
                }

                var existingStation = await _stationService.GetStationByIdAsync(id);
                if (existingStation == null)
                {
                    return NotFound(new { success = false, message = "Station not found." });
                }

                var allStations = await _stationService.GetAllStationsAsync();
                if (allStations.Any(s => s.Name.Equals(updateDto.Name, StringComparison.OrdinalIgnoreCase) && s.Id != id))
                {
                    return Conflict(new { success = false, message = "A station with the same name already exists." });
                }

                var updatedStation = await _stationService.UpdateStationAsync(id, updateDto);
                return Ok(new { success = true, data = updatedStation });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing your request.");
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }

        /// <summary>
        /// Deletes a station by its ID.
        /// <param name="id">ID of the station to delete.</param>
        /// <returns>True if the station was deleted; otherwise, 404 Not Found.</returns>
        /// <response code="200">Station deleted successfully.</response>
        /// <response code="404">Station not found.</response>
        /// <response code="500">Internal server error.</response>
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(Guid id)
        {
            try
            {
                var existingStation = await _stationService.GetStationByIdAsync(id);
                if (existingStation == null)
                {
                    return NotFound(new { success = false, message = "Station not found." });
                }

                var result = await _stationService.DeleteStationAsync(id);
                if (result)
                {
                    return Ok(new { success = true, message = "Station deleted successfully." });
                }
                else
                {
                    return StatusCode(500, new { success = false, message = "Failed to delete the station." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing your request.");
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }


    }
}