using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Developer;
using WebApp.Models.Identity;
using WebApp.Services.Developer;
using WebApp.Services.Files;

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

        public CreateCompany(IDeveloperService developerService, SignInManager<ApplicationUser> signInManager, IFileService fileService, UserManager<ApplicationUser> userManager)
        {
            _developerService = developerService;
            _signInManager = signInManager;
            _fileService = fileService;
            _userManager = userManager;
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
            
            //company by name
            return Redirect("/");
        }
    }
}