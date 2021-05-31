using System.ComponentModel.DataAnnotations;
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
using WebApp.Services.Payment;

namespace WebApp.Pages
{
    public class CompanySettings : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDeveloperService _developerService;
        private readonly IPaymentService _paymentService;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _appEnvironment;

        public CompanySettings(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IDeveloperService developerService, IPaymentService paymentService, IFileService fileService, IWebHostEnvironment appEnvironment)
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
            
            [Display(Name = "Add user")]
            public int NewUserId { get; set; }
        }
        
        [BindProperty]
        public InputModel Input { get; set; }
        
        [BindProperty]
        public int CompanyId { get; set; }
        
        [Display(Name = "Avatar")]
        [BindProperty]
        public IFormFile Avatar { get; set; }
        
        public string AvatarPath { get; set; }
        
        public string StatusMessage { get; set; }
        
        private async Task LoadAsync(CompanyModel company)
        {
            var name = company.Name;
            AvatarPath = (await _fileService.GetAvatar(company.Id, CreatorType.Company)).Path;
            CompanyId = company.Id;

            Input = new InputModel
            {
                Name = name
            };
        }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var company = await _developerService.GetCompany(id);
            var user = await _userManager.GetUserAsync(User);

            if (company is null)
                return NotFound();

            if (user is null)
                return Redirect("/Identity/Account/Login");

            if (!(await _developerService.GetCompanyUsers(id)).Select(u => u.Id).Contains(user.UserId))
                return Forbid();
            
            await LoadAsync(company);
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var id = CompanyId;
            var company = await _developerService.GetCompany(id);
            var user = await _userManager.GetUserAsync(User);
            
            if (company is null)
                return NotFound();

            if (user is null)
                return Redirect("/Identity/Account/Login");

            var companyUsersIds = (await _developerService.GetCompanyUsers(id))
                .Select(u => u.Id)
                .ToList();
            if (!companyUsersIds.Contains(user.UserId))
                return Forbid();
            
            if (!ModelState.IsValid)
            {
                await LoadAsync(company);
                return Page();
            }

            var name = company.Name;
            if (Input.Name != name)
            {
                await _developerService.UpdateCompany(new CompanyModel {Id = company.Id, Name = Input.Name});
            }

            if (Input.NewUserId > 0 && !companyUsersIds.Contains(Input.NewUserId))
            {
                await _developerService.AddUserToCompany(Input.NewUserId, company.Id);
            }
            
            if (Avatar is {ContentType: "image/jpeg" or "image/png"})
            {
                var avatar = new AvatarModel
                {
                    CreatorId = company.Id,
                    CreatorType = CreatorType.Company,
                    Name = $"{company.Id}_{Avatar.FileName}"
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
            return Redirect($"/CompanySettings?id={id}");
        }
    }
}