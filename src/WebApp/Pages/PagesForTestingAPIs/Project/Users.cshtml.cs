using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Project
{
    public class Users : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Users(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        
        public IEnumerable<UserModel> UserModels { get; set; }

        public async Task<ActionResult> OnGetAsync(int projectId)
        {
            UserModels = await _developerService.GetProjectUsers(projectId);
            return Page();
        }
    }
}