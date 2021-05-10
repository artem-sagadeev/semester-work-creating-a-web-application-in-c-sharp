using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Models.Subscription;
using WebApp.Services.Posts;
using WebApp.Services.Subscription;

namespace WebApp.Pages
{
    public class Feed : PageModel
    {
        private readonly IPostsService _postsService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubscriptionService _subscriptionService;

        public Feed(IPostsService postsService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISubscriptionService subscriptionService)
        {
            _postsService = postsService;
            _signInManager = signInManager;
            _userManager = userManager;
            _subscriptionService = subscriptionService;
        }

        public IEnumerable<PostModel> PostModels { get; set; }
        
        public async Task<ActionResult> OnGet()
        {
            if (_signInManager.IsSignedIn(User))
                return Redirect("/Identity/Account/Login");

            var user = await _userManager.GetUserAsync(User);
            var subscriptions = (await _subscriptionService
                    .GetPaidSubscriptionsByUserId(user.UserId))
                    .ToList();
            var subscriptionsToUsers = subscriptions
                .Where(s => s.Tariff.TypeOfSubscription == TypeOfSubscription.User);
            var subscriptionsToProjects = subscriptions
                .Where(s => s.Tariff.TypeOfSubscription == TypeOfSubscription.Project);
            var subscriptionsToCompanies = subscriptions
                .Where(s => s.Tariff.TypeOfSubscription == TypeOfSubscription.Team);
            
            return Page();
        }
    }
}