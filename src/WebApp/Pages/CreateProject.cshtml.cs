using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Areas.Identity.Data;
using WebApp.Models.Developer;
using WebApp.Models.Identity;
using WebApp.Services.Developer;

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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateProject(IDeveloperService developerService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _developerService = developerService;
            _signInManager = signInManager;
            _userManager = userManager;
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

        public async Task<ActionResult> OnPostAsync(ProjectForm projectForm)
        {
            projectForm.UserId = (await _userManager.GetUserAsync(User)).UserId;
            var message = await _developerService.CreateProject(projectForm);

            if (!string.IsNullOrEmpty(message))
                return BadRequest(message);

            var projectId = (await _developerService.GetProject(projectForm.Name)).Id;
            return Redirect($"/ProjectProfile/{projectId}");
        }
    }
}