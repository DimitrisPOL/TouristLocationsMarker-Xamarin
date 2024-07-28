using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Locations.Data.Data;
using Locations.Models.Models;

namespace Topothesies.Pages.Locations
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        private readonly ILogger<EditModel> _logger;


        public EditModel(ApplicationDbContext context, IHostingEnvironment environment, ILogger<EditModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        [BindProperty]
        public IFormFile ImageUpload { get; set; }
        [BindProperty]
        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Locations.Include(l => l.Area).Include(l => l.Coordinates).FirstOrDefaultAsync(m => m.Id == id);

            if (Location == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Location).State = EntityState.Modified;

            try
            {

                if (Upload != null)
                {
                    var file = Path.Combine(_environment.ContentRootPath, @"wwwroot\uploads", Upload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);

                    }
                    Location.FilePath = Path.Combine(@"\", @"\uploads", Upload.FileName);
                }

                if (ImageUpload != null)
                {
                    var file = Path.Combine(_environment.ContentRootPath, @"wwwroot\uploads", ImageUpload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await ImageUpload.CopyToAsync(fileStream);

                    }
                    Location.ImagePath = Path.Combine(@"\", @"\uploads", ImageUpload.FileName);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!LocationExists(Location.Id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(dbEx.Message);
                    throw;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return RedirectToPage("./IndexLoc");
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
