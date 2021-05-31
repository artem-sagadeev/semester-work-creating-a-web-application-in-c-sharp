using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Identity;
using WebApp.Models.Subscription;
using WebApp.Services.Developer;
using WebApp.Services.Subscription;

namespace WebApp.Pages
{
    public class SubscriptionsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDeveloperService _developerService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public List<PaidSubscriptionModel> AllSubscriptions;
        public SubscriptionsModel(UserManager<ApplicationUser> userManager, IDeveloperService developerService, ISubscriptionService subscriptionService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _developerService = developerService;
            _subscriptionService = subscriptionService;
            _signInManager = signInManager;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var developerId = (await _userManager.GetUserAsync(User)).UserId;
                if (developerId != null)
                {
                    AllSubscriptions = (await _subscriptionService.GetPaidSubscriptionsByUserId(developerId)).ToList();
                }

                return Page();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}
