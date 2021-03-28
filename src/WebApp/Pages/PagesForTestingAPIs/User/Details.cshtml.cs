using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.User
{
    public class Details : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Details(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public UserModel UserModel { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            UserModel = await _developerService.GetUser(id);
            return Page();
        }
    }
}