using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Developer;
using WebApp.Models.Files;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Services;
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Posts;

namespace WebApp.Pages
{
    public class UserProfile : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IPostsService _postsService;
        private readonly IFileService _fileService;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfile(IDeveloperService developerService, 
            IPostsService postsService, 
            UserManager<ApplicationUser> userManager, 
            IFileService fileService)
        {
            _developerService = developerService;
            _postsService = postsService;
            _userManager = userManager;
            _fileService = fileService;
        }

        public UserModel UserModel { get; private set; }
        public IEnumerable<PostModel> PostModels { get; private set; }
        public AvatarModel Avatar { get; private set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            UserModel = await _developerService.GetUser(id);
            UserModel.Tags = await _developerService.GetTags(UserModel);
            UserModel.Companies = await _developerService.GetUserCompanies(id);
            UserModel.Projects = await _developerService.GetUserProjects(id);
            PostModels = await _postsService.GetUserPosts(id);
            Avatar = await _fileService.GetAvatar(UserModel.Id, CreatorType.User);
            return Page();
        }

        public async Task OnPostFollowToUserAsync(int developerId, int userId)
        {
            var handler = new SubscribeHandler();
            await handler.FollowToUser(userId, developerId);
        }

        public async Task OnPostSubscribeToUserAsync(int userId, int developerId, bool isBasic, bool isImproved, bool isMax)
        {
            var handler = new SubscribeHandler();
            await handler.SubscribeToUser(userId, developerId, isBasic, isImproved, isMax);
        }

        public async Task<IActionResult> OnPostAsync(int id, string text)
        {
            //todo add image
            //todo add files
            if ((await _userManager.GetUserAsync(User)).UserId != id)
                return Forbid();
            
            var post = new PostModel {UserId = id, Text = text};
            await _postsService.CreatePost(post);
            return Redirect($"/UserProfile?id={id}");
        }
    }
}