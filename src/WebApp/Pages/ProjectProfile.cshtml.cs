using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Developer;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Services;
using WebApp.Services.Developer;
using WebApp.Services.Posts;

namespace WebApp.Pages
{
    public class ProjectProfile : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IPostsService _postsService;
        
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectProfile(IDeveloperService developerService, IPostsService postsService, UserManager<ApplicationUser> userManager)
        {
            _developerService = developerService;
            _postsService = postsService;
            _userManager = userManager;
        }

        public ProjectModel ProjectModel { get; private set; }
        public IEnumerable<PostModel> PostModels { get; private set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            ProjectModel = await _developerService.GetProject(id);
            ProjectModel.Tags = await _developerService.GetTags(ProjectModel);
            ProjectModel.Company = await _developerService.GetProjectCompany(id);
            ProjectModel.Users = await _developerService.GetProjectUsers(id);
            PostModels = await _postsService.GetProjectPosts(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, string text)
        {
            //todo add image
            //todo add files
            if (!ProjectModel.Users.Select(u => u.Id).Contains((await _userManager.GetUserAsync(User)).UserId))
                return Forbid();
            
            var post = new PostModel {ProjectId = id, Text = text};
            await _postsService.CreatePost(post);
            return Redirect($"/ProjectProfile?id={id}");
        }
    }
}