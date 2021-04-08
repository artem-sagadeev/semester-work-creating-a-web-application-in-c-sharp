using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Users
{
    public class Details : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IPostsService _postsService;

        public Details(IDeveloperService developerService, IPostsService postsService)
        {
            _developerService = developerService;
            _postsService = postsService;
        }

        public UserModel UserModel { get; set; }
        public IEnumerable<ProjectModel> ProjectModels { get; set; }
        public IEnumerable<PostModel> PostModels { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            UserModel = await _developerService.GetUser(id);
            UserModel.Tags = await _developerService.GetTags(UserModel);
            UserModel.Companies = await _developerService.GetUserCompanies(id);
            ProjectModels = await _developerService.GetUserProjects(id);
            PostModels = await _postsService.GetUserPosts(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, string text)
        {
            var post = new PostModel {UserId = id, Text = text};
            await _postsService.CreatePost(post);
            return Redirect($"/Users/Details?id={id}");
        }
    }
}