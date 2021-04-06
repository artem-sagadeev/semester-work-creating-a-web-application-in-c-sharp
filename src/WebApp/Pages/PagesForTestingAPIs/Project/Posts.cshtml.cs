using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Project
{
    public class Posts : PageModel
    {
        private readonly IPostsService _postsService;

        public Posts(IPostsService postsService)
        {
            _postsService = postsService;
        }
        
        public IEnumerable<PostModel> PostModels { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            PostModels = await _postsService.GetGroupPosts(id);
            return Page();
        }
    }
}