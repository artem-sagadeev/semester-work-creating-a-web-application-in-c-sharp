using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Comment
{
    public class Details : PageModel
    {
        private readonly IPostsService _postsService;

        public Details(IPostsService postsService)
        {
            _postsService = postsService;
        }
        
        public CommentModel CommentModel { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            CommentModel = await _postsService.GetComment(id);
            return Page();
        }
    }
}