using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.User
{
    public class Projects : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Projects(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public IEnumerable<ProjectModel> ProjectModels { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int userId)
        {
            ProjectModels = await _developerService.GetUserProjects(userId);
            return Page();
        }
    }
}