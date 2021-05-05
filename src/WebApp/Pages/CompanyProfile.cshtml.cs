using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Developer;
using WebApp.Services;
using WebApp.Services.Developer;

namespace WebApp.Pages
{
    public class CompanyProfile : PageModel
    {
        private readonly IDeveloperService _developerService;

        public CompanyProfile(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public CompanyModel CompanyModel { get; private set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            CompanyModel = await _developerService.GetCompany(id);

            if (CompanyModel is null)
                return NotFound();
            
            CompanyModel.Tags = await _developerService.GetTags(CompanyModel) ?? new List<TagModel>();
            CompanyModel.Users = await _developerService.GetCompanyUsers(id) ?? new List<UserModel>();
            CompanyModel.Projects = await _developerService.GetCompanyProjects(id) ?? new List<ProjectModel>();
            return Page();
        }
    }
}