using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class UserProfile : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IPostsService _postsService;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfile(IDeveloperService developerService, IPostsService postsService, UserManager<ApplicationUser> userManager)
        {
            _developerService = developerService;
            _postsService = postsService;
            _userManager = userManager;
        }

        public UserModel UserModel { get; set; }
        public IEnumerable<PostModel> PostModels { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            UserModel = await _developerService.GetUser(id);
            UserModel.Tags = await _developerService.GetTags(UserModel);
            UserModel.Companies = await _developerService.GetUserCompanies(id);
            UserModel.Projects = await _developerService.GetUserProjects(id);
            PostModels = await _postsService.GetUserPosts(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, string text)
        {
            if ((await _userManager.GetUserAsync(User)).UserId != id)
                return Forbid();
            
            var post = new PostModel {UserId = id, Text = text};
            await _postsService.CreatePost(post);
            return Redirect($"/UserProfile?id={id}");
        }
    }
}