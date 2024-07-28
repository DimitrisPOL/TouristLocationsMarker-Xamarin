using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Locations.Data.Data;
using Locations.Models.Models;
using Locations.UI.Configuration;

namespace Topothesies.Pages.Locations
{
    [Authorize]
    public class IndexLocModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexLocModel> _logger;
        
        public IndexLocModel(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            mapCenter = ApplicationConfiguration.MapCenter;
            _logger = loggerFactory.CreateLogger<IndexLocModel>();
        }

        public IList<Location> Location { get;set; }
        public readonly MapCenter mapCenter;
        public async Task OnGetAsync()
        {
            try
            {
                Location = await _context.Locations.Include(l => l.Area).Include(l => l.Coordinates).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
