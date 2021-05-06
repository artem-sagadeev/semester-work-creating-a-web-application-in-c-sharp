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
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Posts;

namespace WebApp.Pages
{
    public class CompanyForm
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
    
    public class CreateCompany : PageModel
    {
        private readonly IDeveloperService _developerService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _appEnvironment;

        public CreateCompany(IDeveloperService developerService, SignInManager<ApplicationUser> signInManager, IFileService fileService, IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager)
        {
            _developerService = developerService;
            _signInManager = signInManager;
            _fileService = fileService;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        public ActionResult OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPostAsync(CompanyForm companyForm, IFormFile avatar)
        {
            if (!_signInManager.IsSignedIn(User))
                return Forbid();
            
            companyForm.UserId = (await _userManager.GetUserAsync(User)).UserId;
            var message = await _developerService.CreateCompany(companyForm);
            
            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            var companyId = (await _developerService.GetCompany(companyForm.Name)).Id;

            if (avatar is not null)
            {
                var path = $"/avatars/{companyForm.Name}.{avatar.FileName.Split(".").Last()}";
                await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await avatar.CopyToAsync(fileStream);
                }

                await _fileService.CreateAvatar(new AvatarModel()
                {
                    CreatorId = companyId,
                    Name = $"{companyForm.Name}.{avatar.FileName.Split(".").Last()}",
                    CreatorType = CreatorType.Company
                });
            }
            
            return Redirect($"/CompanyProfile?id={companyId}");
        }
    }
}