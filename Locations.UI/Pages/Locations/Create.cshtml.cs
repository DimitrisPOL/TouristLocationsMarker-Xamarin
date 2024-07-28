using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Locations.Data.Data;
using Locations.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Topothesies.Pages.Locations
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ApplicationDbContext context, IHostingEnvironment environment, ILogger<CreateModel> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }
        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if(Upload != null)
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

                _context.Locations.Add(Location);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToPage("./IndexLoc");
        }
    }
}
