using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using WebApp.Models.Developer;
using WebApp.Models.Files;
using WebApp.Models.Identity;
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Payment;

namespace WebApp.Pages
{
    public class ProjectSettings : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDeveloperService _developerService;
        private readonly IPaymentService _paymentService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProjectSettings(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDeveloperService developerService, IPaymentService paymentService, IFileService fileService, IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _developerService = developerService;
            _paymentService = paymentService;
            _fileService = fileService;
            _appEnvironment = appEnvironment;
        }
        
        public class InputModel
        {
            [Display(Name = "Name")]
            public string Name { get; set; }
        }
        
        [BindProperty]
        public int ProjectId { get; set; }
        
        [Display(Name = "Avatar")]
        [BindProperty]
        public IFormFile Avatar { get; set; }
        
        public string AvatarPath { get; set; }
        
        [BindProperty]
        public InputModel Input { get; set; }
        
        public string StatusMessage { get; set; }
        
        private async Task LoadAsync(ProjectModel project)
        {
            var name = project.Name;
            AvatarPath = (await _fileService.GetAvatar(project.Id, CreatorType.Project)).Path;
            ProjectId = project.Id;

            Input = new InputModel
            {
                Name = name
            };
        }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var project = await _developerService.GetProject(id);
            var user = await _userManager.GetUserAsync(User);

            if (project is null)
                return NotFound();

            if (user is null)
                return Redirect("/Identity/Account/Login");

            if (!(await _developerService.GetProjectUsers(id)).Select(u => u.Id).Contains(user.UserId))
                return Forbid();
            
            await LoadAsync(project);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = ProjectId;
            var project = await _developerService.GetProject(id);
            var user = await _userManager.GetUserAsync(User);
            
            if (project is null)
                return NotFound();

            if (user is null)
                return Redirect("/Identity/Account/Login");

            if (!(await _developerService.GetProjectUsers(id)).Select(u => u.Id).Contains(user.UserId))
                return Forbid();
            
            if (!ModelState.IsValid)
            {
                await LoadAsync(project);
                return Page();
            }

            var name = project.Name;
            if (Input.Name != name)
            {
                await _developerService.UpdateProject(new ProjectModel {Id = project.Id, Name = Input.Name});
            }
            
            if (Avatar is {ContentType: "image/jpeg" or "image/png"})
            {
                var avatar = new AvatarModel
                {
                    CreatorId = project.Id,
                    CreatorType = CreatorType.Project,
                    Name = $"{project.Id}_{Avatar.FileName}"
                };
                
                
                var path = $"/avatars/{avatar.Name}";
                await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await Avatar.CopyToAsync(fileStream);
                }

                await _fileService.CreateAvatar(avatar);
            }
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return Redirect($"/ProjectSettings?id={id}");
        }
    }
}