using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Developer;
using WebApp.Models.Files;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Models.Subscription;
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Posts;
using WebApp.Services.Subscription;

namespace WebApp.Pages
{
    public class Post : PageModel
    {
        private readonly IPostsService _postsService;
        private readonly IFileService _fileService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IDeveloperService _developerService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public Post(IPostsService postService, IFileService fileService, ISubscriptionService subscriptionService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IDeveloperService developerService, IWebHostEnvironment appEnvironment)
        {
            _postsService = postService;
            _fileService = fileService;
            _subscriptionService = subscriptionService;
            _signInManager = signInManager;
            _userManager = userManager;
            _developerService = developerService;
            _appEnvironment = appEnvironment;
        }

        public PostModel PostModel { get; set; }
        public CoverModel CoverModel { get; set; }
        public IEnumerable<FileModel> Files { get; set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            PostModel = await _postsService.GetPost(id);
            var user = _signInManager.IsSignedIn(User) ? await _userManager.GetUserAsync(User) : null;

            if (!await PostModel.HasUserAccessAsync(user, _subscriptionService, _developerService))
                return Forbid();
            
            PostModel = await _postsService.GetPost(id);
            CoverModel = await _fileService.GetCover(id);
            Files = await _fileService.GetPostFiles(id);
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(int postId, int projectId, string type, string text, IFormFile cover, IFormFileCollection files)
        {
            var project = await _developerService.GetProject(projectId);

            if (project is null)
                return NotFound();

            project.Users = await _developerService.GetProjectUsers(projectId) ?? new List<UserModel>();
            var userId = (await _userManager.GetUserAsync(User)).UserId;

            if (!project.Users.Select(u => u.Id).Contains(userId))
                return Forbid();
            
            var requiredType = type switch
            {
                "free" => PriceType.Free,
                "basic" => PriceType.Basic,
                "improved" => PriceType.Improved,
                "max" => PriceType.Max,
                _ => throw new NotSupportedException()
            };

            var post = await _postsService.GetPost(postId);
            post.RequiredSubscriptionType = requiredType;
            post.Text = text;

            /*if (cover is not null)
            {
                var path = $"/covers/{project.Id}_{cover.FileName}";
                await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await cover.CopyToAsync(fileStream);
                }
                
                await _fileService.CreateCover(new CoverModel
                {
                    PostId = post.Id,
                    Name = $"{project.Id}_{cover.FileName}"
                });
            }

            foreach (var file in files)
            {
                if (file is not null)
                {
                    var path = $"/files/{project.Id}_{file.FileName}";
                    await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    await _fileService.CreateFile(new FileModel
                    {
                        PostId = createdPost.Id,
                        Name = $"{ProjectModel.Id}_{file.FileName}"
                    });
                }
            }*/
            
            return Redirect($"/Post?id={postId}");
        }
    }
}