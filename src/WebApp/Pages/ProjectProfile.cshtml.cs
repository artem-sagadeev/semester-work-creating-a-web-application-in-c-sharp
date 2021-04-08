using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

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

        public ProjectModel ProjectModel { get; set; }
        public IEnumerable<PostModel> PostModels { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            ProjectModel = await _developerService.GetProject(id);
            ProjectModel.Tags = await _developerService.GetTags(ProjectModel);
            ProjectModel.Company = await _developerService.GetProjectCompany(id);
            ProjectModel.Users = await _developerService.GetProjectUsers(id);
            PostModels = await _postsService.GetGroupPosts(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, string text)
        {
            //todo check
            
            var post = new PostModel {GroupId = id, Text = text};
            await _postsService.CreatePost(post);
            return Redirect($"/ProjectProfile?id={id}");
        }
    }
}