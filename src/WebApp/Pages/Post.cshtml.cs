using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Files;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Posts;
using WebApp.Services.Subscription;

namespace WebApp.Pages
{
    public class Post : PageModel
    {
        private readonly IPostsService _postsService;
        private readonly IFileService _fileService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IDeveloperService _developerService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Post(IPostsService postService, IFileService fileService, ISubscriptionService subscriptionService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IDeveloperService developerService)
        {
            _postsService = postService;
            _fileService = fileService;
            _subscriptionService = subscriptionService;
            _signInManager = signInManager;
            _userManager = userManager;
            _developerService = developerService;
        }

        public PostModel PostModel { get; set; }
        public CoverModel CoverModel { get; set; }
        public IEnumerable<FileModel> Files { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            PostModel = await _postsService.GetPost(id);
            var user = _signInManager.IsSignedIn(User) ? await _userManager.GetUserAsync(User) : null;

            if (!await PostModel.HasUserAccessAsync(user, _subscriptionService, _developerService))
                return Forbid();
            
            PostModel = await _postsService.GetPost(id);
            CoverModel = await _fileService.GetCover(id);
            Files = await _fileService.GetPostFiles(id);
            
            return Page();
        }
    }
}