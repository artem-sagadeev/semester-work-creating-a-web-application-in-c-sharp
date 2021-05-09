using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Services.Posts;

namespace WebApp.Pages
{
    public class Feed : PageModel
    {
        private readonly IPostsService _postsService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Feed(IPostsService postsService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _postsService = postsService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IEnumerable<PostModel> PostModels { get; set; }
        
        public async Task<ActionResult> OnGet()
        {
            if (_signInManager.IsSignedIn(User))
                return Redirect("/Identity/Account/Login");

            var user = await _userManager.GetUserAsync(User);
            //PostModels =
            return Page();
        }
    }
}