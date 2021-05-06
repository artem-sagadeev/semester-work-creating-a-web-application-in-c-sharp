using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Areas.Identity.Data;
using WebApp.Models.Developer;
using WebApp.Models.Files;
using WebApp.Models.Identity;
using WebApp.Services.Developer;
using WebApp.Services.Files;

namespace WebApp.Pages
{
    public class ProjectForm
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
        //todo add tags
        public int UserId { get; set; }
    }
    
    public class CreateProject : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateProject(IDeveloperService developerService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IWebHostEnvironment appEnvironment, IFileService fileService)
        {
            _developerService = developerService;
            _signInManager = signInManager;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
            _fileService = fileService;
        }
        
        public IEnumerable<CompanyModel> UserCompanies { get; set; }
        
        public async Task<ActionResult> OnGetAsync()
        {
            if (!_signInManager.IsSignedIn(User))
                return Redirect("/Identity/Account/Login");

            var userId = (await _userManager.GetUserAsync(User)).UserId;
            UserCompanies = await _developerService.GetUserCompanies(userId);
            
            return Page();
        }

        public async Task<ActionResult> OnPostAsync(ProjectForm projectForm, IFormFile avatar)
        {
            if (!_signInManager.IsSignedIn(User))
                return Forbid();
            
            projectForm.UserId = (await _userManager.GetUserAsync(User)).UserId;
            var message = await _developerService.CreateProject(projectForm);

            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            var projectId = (await _developerService.GetProject(projectForm.Name)).Id;

            if (avatar is not null)
            {
                var path = $"/avatars/{projectForm.Name}.{avatar.FileName.Split(".").Last()}";
                await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await avatar.CopyToAsync(fileStream);
                }

                await _fileService.CreateAvatar(new AvatarModel
                {
                    CreatorId = projectId,
                    Name = $"{projectForm.Name}.{avatar.FileName.Split(".").Last()}",
                    CreatorType = CreatorType.Project
                });
            }

            return Redirect($"/ProjectProfile?id={projectId}");
        }
    }
}