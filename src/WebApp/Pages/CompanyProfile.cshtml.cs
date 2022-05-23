using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Developer;
using WebApp.Models.Identity;
using WebApp.Models.Subscription;
using WebApp.Services;
using WebApp.Services.Developer;

namespace WebApp.Pages
{
    public class CompanyProfile : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IServiceProvider _serviceProvider;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompanyProfile(IDeveloperService developerService, 
            IServiceProvider serviceProvider, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager)
        {
            _developerService = developerService;
            _serviceProvider = serviceProvider;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public CompanyModel CompanyModel { get; private set; }

        public async Task<IActionResult> OnPostFollowAsync(int userId, int subscribedToId, TypeOfSubscription typeOfSubscription)
        {
            var handler = new SubscribeHandler(_serviceProvider);
            await handler.Follow(userId, subscribedToId, typeOfSubscription);
            return Redirect($"/CompanyProfile?id={subscribedToId}");
        }

        public async Task<IActionResult> OnPostSubscribeAsync(int subscribedToId, int userId, bool isBasic, bool isImproved, bool isMax, TypeOfSubscription typeOfSubscription)
        {
            var handler = new SubscribeHandler(_serviceProvider);
            await handler.Subscribe(userId, subscribedToId, isBasic, isImproved, isMax, typeOfSubscription);
            return Redirect($"/CompanyProfile?id={subscribedToId}");
        }

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

        public async Task<bool> CheckAccess(CompanyModel company)
        {
            if (!_signInManager.IsSignedIn(User))
                return false;

            var userId = (await _userManager.GetUserAsync(User)).UserId;
            
            var companyUsersIds = (await _developerService.GetCompanyUsers(company.Id))
                .Select(u => u.Id)
                .ToList();
            
            return companyUsersIds.Contains(userId);
        }
    }
}