using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Post
{
    public class Details : PageModel
    {
        private readonly IPostsService _postsService;

        public Details(IPostsService postsService)
        {
            _postsService = postsService;
        }

        public PostModel PostModel { get; set; }
        public IEnumerable<CommentModel> CommentModels { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            PostModel = await _postsService.GetPost(id);
            CommentModels = await _postsService.GetPostComments(id);
            return Page();
        }
    }
}