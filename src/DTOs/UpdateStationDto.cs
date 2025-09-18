using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_stations_service.src.Models;

namespace perla_metro_stations_service.src.DTOs
{
    public class UpdateStationDto
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public StationType? Type { get; set; }

        public bool? IsActive { get; set; }
    }
}