using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Project
{
    public class Details : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Details(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public ProjectModel ProjectModel { get; set; }
        public CompanyModel CompanyModel { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            ProjectModel = await _developerService.GetProject(id);
            CompanyModel = await _developerService.GetProjectCompany(id);
            return Page();
        }
    }
}