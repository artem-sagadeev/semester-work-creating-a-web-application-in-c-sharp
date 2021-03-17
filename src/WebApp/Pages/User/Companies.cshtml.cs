using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.User
{
    public class Companies : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Companies(IDeveloperService developerService)
        {
            _developerService = developerService;
        }
        
        public IEnumerable<CompanyModel> UserCompanies { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            UserCompanies = await _developerService.GetUserCompanies(id);
            return Page();
        }
    }
}