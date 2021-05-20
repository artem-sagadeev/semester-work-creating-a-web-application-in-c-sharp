using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Controller;
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

        public async Task OnPostFollowToCompanyAsync(int companyId, int userId)
        {
            var handler = new SubscribeHandler();
            await handler.FollowToCompany(userId, companyId);
        }

        public async Task OnPostSubscribeToCompanyAsync(int userId, int companyId, bool isBasic, bool isImproved, bool isMax)
        {
            var handler = new SubscribeHandler();
            await handler.SubscribeToCompany(userId, companyId, isBasic, isImproved, isMax);
        }
        public async Task<ActionResult> OnGetAsync(int id)
        {
            CompanyModel = await _developerService.GetCompany(id);
            CompanyModel.Tags = await _developerService.GetTags(CompanyModel);
            CompanyModel.Users = await _developerService.GetCompanyUsers(id);
            CompanyModel.Projects = await _developerService.GetCompanyProjects(id);
            return Page();
        }
    }
}