using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.User
{
    public class Index : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Index(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public IEnumerable<UserModel> UserModels { get; set; }
        
        public async Task<ActionResult> OnGetAsync()
        {
            UserModels = await _developerService.GetUsers();
            return Page();
        }
    }
}