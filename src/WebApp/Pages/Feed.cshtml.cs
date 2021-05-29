using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Developer;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Models.Subscription;
using WebApp.Services.Developer;
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
        private readonly IDeveloperService _developerService;

        public Feed(IPostsService postsService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ISubscriptionService subscriptionService, IDeveloperService developerService)
        {
            _postsService = postsService;
            _signInManager = signInManager;
            _userManager = userManager;
            _subscriptionService = subscriptionService;
            _developerService = developerService;
        }

        public IEnumerable<PostModel> PostModels { get; set; }
        
        public async Task<ActionResult> OnGet()
        {
            if (!_signInManager.IsSignedIn(User))
                return Redirect("/Identity/Account/Login");

            var user = await _userManager.GetUserAsync(User);
            var subscriptions = (await _subscriptionService
                    .GetPaidSubscriptionsByUserId(user.UserId))
                    .ToList();

            var posts = new List<PostModel>();
            
            var userIds = subscriptions
                .Where(s => s.Tariff.TypeOfSubscription == TypeOfSubscription.User)
                .Select(s => s.SubscribedToId);
            var projectIds = subscriptions
                .Where(s => s.Tariff.TypeOfSubscription == TypeOfSubscription.Project)
                .Select(s => s.SubscribedToId);
            var companyIds = subscriptions
                .Where(s => s.Tariff.TypeOfSubscription == TypeOfSubscription.Team)
                .Select(s => s.SubscribedToId);

            foreach (var id in companyIds)
                projectIds = projectIds
                    .Concat((await _developerService.GetCompanyProjects(id)).Select(p => p.Id));

            projectIds = projectIds.Distinct();
            
            foreach (var id in userIds)
                posts.AddRange(await _postsService.GetUserPosts(id) ?? new List<PostModel>());
            foreach (var id in projectIds)
                posts.AddRange(await _postsService.GetProjectPosts(id) ?? new List<PostModel>());

            PostModels = posts.OrderByDescending(p => p.Id).Distinct();
            
            return Page();
        }
    }
}