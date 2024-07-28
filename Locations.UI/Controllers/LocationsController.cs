using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Locations.Models.Models;
using Locations.Data.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locations.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        // GET: api/<ValuesController>

        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("getLocations")]
        public async Task<IEnumerable<Location>> Post()
        {

            var result = await _context.Locations.Include(l => l.Coordinates).ToListAsync();
            return result;
        }

        [HttpPost("getLocationsByDate")]
        public async Task<IEnumerable<Location>> PostByDate(LongRequest ticks)
        {
            var date = new DateTime(ticks.Ticks);
            var result = await _context.Locations.Include(l => l.Coordinates).Where(l => DateTime.Compare(l.CreatedAt, date)>0 ).ToListAsync();
            return result;
        }

        [HttpPost("getLocationsByAreaName")]
        public async Task<IEnumerable<Location>> PostByAreaName(string areaName)
        {

            return await _context.Locations.Include(l => l.Area).Include(l => l.Coordinates).Where(l => l.Area.AreaName == areaName).ToListAsync();
        }

        [HttpPost("getLocationsByAreaCode")]
        public async Task<IEnumerable<Location>> PostByAreaCode(int areaCode)
        {

            return await _context.Locations.Include(l => l.Area).Include(l => l.Coordinates).Where(l => l.Area.AreaCode == areaCode).ToListAsync();
        }
    }
    public class LongRequest
    {
        public long Ticks { get; set; }
        public LongRequest()
        {

        }

    }
}
