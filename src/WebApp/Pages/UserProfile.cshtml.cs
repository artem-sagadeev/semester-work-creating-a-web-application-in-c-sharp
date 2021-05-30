using System;
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
using WebApp.Models.Subscription;
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
        private readonly IServiceProvider _serviceProvider;


        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfile(IDeveloperService developerService,
            IPostsService postsService,
            UserManager<ApplicationUser> userManager,
            IFileService fileService, IServiceProvider serviceProvider)
        {
            _developerService = developerService;
            _postsService = postsService;
            _userManager = userManager;
            _fileService = fileService;
            _serviceProvider = serviceProvider;
        }

        public UserModel UserModel { get; private set; }

        public IEnumerable<PostModel> PostModels { get; private set; }

        public AvatarModel Avatar { get; private set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            UserModel = await _developerService.GetUser(id);

            if (UserModel is null)
                return NotFound();
            UserModel.Tags = await _developerService.GetTags(UserModel) ?? new List<TagModel>();
            UserModel.Companies = await _developerService.GetUserCompanies(id) ?? new List<CompanyModel>();
            UserModel.Projects = await _developerService.GetUserProjects(id) ?? new List<ProjectModel>();
            PostModels = await _postsService.GetUserPosts(id) ?? new List<PostModel>();
            Avatar = await _fileService.GetAvatar(UserModel.Id, CreatorType.User);
            return Page();
        }

        public async Task<IActionResult> OnPostFollowAsync(int userId, int subscribedToId,
            TypeOfSubscription typeOfSubscription)
        {
            var handler = new SubscribeHandler(_serviceProvider);
            await handler.Follow(userId, subscribedToId, typeOfSubscription);
            return Redirect($"/UserProfile?id={subscribedToId}");
        }

        public async Task<IActionResult> OnPostSubscribeAsync(int subscribedToId, int userId, bool isBasic,
            bool isImproved, bool isMax, TypeOfSubscription typeOfSubscription)
        {
            var handler = new SubscribeHandler(_serviceProvider);
            await handler.Subscribe(userId, subscribedToId, isBasic, isImproved, isMax, typeOfSubscription);
            return Redirect($"/UserProfile?id={subscribedToId}");

        }

        public async Task<IActionResult> OnPostAsync(int id, string text)
        {
            //todo add image
            //todo add files
            if ((await _userManager.GetUserAsync(User)).UserId != id)
                return Forbid();
        }
    }

}