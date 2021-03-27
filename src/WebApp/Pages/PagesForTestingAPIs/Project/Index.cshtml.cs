using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Project
{
    public class Index : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Index(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public IEnumerable<ProjectModel> ProjectModels { get; set; }
        
        public async Task<ActionResult> OnGetAsync()
        {
            ProjectModels = await _developerService.GetProjects();
            return Page();
        }
    }
}