using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Company
{
    public class Details : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Details(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public CompanyModel CompanyModel { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            CompanyModel = await _developerService.GetCompany(id);
            return Page();
        }
    }
}