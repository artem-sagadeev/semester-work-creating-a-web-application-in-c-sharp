using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using WebApp.Models;
using WebApp.Models.Chats;
using WebApp.Models.Developer;
using WebApp.Models.Files;
using WebApp.Models.Identity;
using WebApp.Models.Posts;
using WebApp.Models.Subscription;
using WebApp.Services;
using WebApp.Services.Chats;
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Posts;

namespace WebApp.Pages
{
    public class ProjectProfile : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IPostsService _postsService;
        private readonly IChatService _chatService;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IFileService _fileService;
        
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectProfile(IDeveloperService developerService, IPostsService postsService, IChatService chatService, UserManager<ApplicationUser> userManager, IWebHostEnvironment appEnvironment, IFileService fileService)
        {
            _developerService = developerService;
            _postsService = postsService;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
            _fileService = fileService;
            _chatService = chatService;
        }

        public ProjectModel ProjectModel { get; private set; }
        public IEnumerable<PostModel> PostModels { get; private set; }
        public List<(string, MessageModel)> Messages { get; private set; }
        
        public async Task<ActionResult> OnGetAsync(int id)
        {
            ProjectModel = await _developerService.GetProject(id);

            if (ProjectModel is null)
                return NotFound();
            
            ProjectModel.Tags = await _developerService.GetTags(ProjectModel) ?? new List<TagModel>();
            ProjectModel.Company = await _developerService.GetProjectCompany(id);
            ProjectModel.Users = await _developerService.GetProjectUsers(id) ?? new List<UserModel>();
            PostModels = await _postsService.GetProjectPosts(id) ?? new List<PostModel>();
            Messages = await GetAllMessages(id);
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(int id, string type, string text, IFormFile cover, IFormFileCollection files)
        {
            //todo add image
            //todo add files
            ProjectModel = await _developerService.GetProject(id);

            if (ProjectModel is null)
                return NotFound();
            
            ProjectModel.Users = await _developerService.GetProjectUsers(id) ?? new List<UserModel>();
            var userId = (await _userManager.GetUserAsync(User)).UserId;
            
            if (!ProjectModel.Users.Select(u => u.Id).Contains(userId))
                return Forbid();
            
            var requiredType = type switch
            {
                "free" => PriceType.Free,
                "basic" => PriceType.Basic,
                "improved" => PriceType.Improved,
                "max" => PriceType.Max,
                _ => throw new NotSupportedException()
            };

            var post = new PostModel {ProjectId = id, UserId = userId, RequiredSubscriptionType = requiredType, Text = text};
            var createdPost = await _postsService.CreatePost(post);

            if (cover is not null)
            {
                var path = $"/covers/{ProjectModel.Id}_{cover.FileName}";
                await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await cover.CopyToAsync(fileStream);
                }

                await _fileService.CreateCover(new CoverModel
                {
                    PostId = createdPost.Id,
                    Name = $"{ProjectModel.Id}_{cover.FileName}"
                });
            }

            foreach (var file in files)
            {
                if (file is not null)
                {
                    var path = $"/files/{ProjectModel.Id}_{file.FileName}";
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
            }
            
            return Redirect($"/ProjectProfile?id={id}");
        }

        public async Task<List<(string, MessageModel)>> GetAllMessages(int projectId)
        {
            var messages = new List<(string, MessageModel)>();
            var allMesages = (await _chatService.GetMessagesByProjectId(projectId)).OrderBy(x=> x.DateTime);
            foreach (var message in allMesages)
            {
                var userId = message.UserId;
                var name = (await _developerService.GetUser(userId)).Name;
                //var name = userId.ToString();
                messages.Add((name, message));
            }

            return messages;
        }
    }
}