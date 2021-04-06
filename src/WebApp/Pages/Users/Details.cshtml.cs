using System.Collections.Generic;
using System.Threading.Tasks;
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
            ProjectModels = await _developerService.GetUserProjects(id);
            PostModels = await _postsService.GetUserPosts(id);
            return Page();
        }
    }
}