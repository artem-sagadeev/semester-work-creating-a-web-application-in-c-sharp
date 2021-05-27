using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Developer;
using WebApp.Models.Subscription;
using WebApp.Services;
using WebApp.Services.Developer;

namespace WebApp.Pages
{
    public class CompanyProfile : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IServiceProvider _serviceProvider;


        public CompanyProfile(IDeveloperService developerService, IServiceProvider serviceProvider)
        {
            _developerService = developerService;
            _serviceProvider = serviceProvider;
        }

        public CompanyModel CompanyModel { get; private set; }

        public async Task OnPostFollowAsync(int userId, int subscribedToId, TypeOfSubscription typeOfSubscription)
        {
            var handler = new SubscribeHandler(_serviceProvider);
            await handler.Follow(userId, subscribedToId, typeOfSubscription);
        }

        public async Task OnPostSubscribeAsync(int subscribedToId, int userId, bool isBasic, bool isImproved, bool isMax, TypeOfSubscription typeOfSubscription)
        {
            var handler = new SubscribeHandler(_serviceProvider);
            await handler.Subscribe(userId, subscribedToId, isBasic, isImproved, isMax, typeOfSubscription);
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