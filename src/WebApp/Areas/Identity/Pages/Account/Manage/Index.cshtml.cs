using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Areas.Identity.Data;
using WebApp.Models;
using WebApp.Models.Files;
using WebApp.Models.Identity;
using WebApp.Services.Developer;
using WebApp.Services.Files;
using WebApp.Services.Payment;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDeveloperService _developerService;
        private readonly IPaymentService _paymentService;
        private readonly IFileService _fileService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IDeveloperService developerService, IPaymentService paymentService, IFileService fileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _developerService = developerService;
            _paymentService = paymentService;
            _fileService = fileService;
        }

        [TempData]
        public string StatusMessage { get; set; }
        
        public string AvatarPath { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        
        [Display(Name = "Avatar")]
        [BindProperty]
        public IFormFile Avatar { get; set; }

        public class InputModel
        {
            [Display(Name = "Name")]
            public string Name { get; set; }
            
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            
            [CreditCard]
            [Display(Name = "Card")]
            public string Card { get; set; }
        }

        private async Task LoadAsync(ApplicationUser applicationUser)
        {
            var email = await _userManager.GetUserNameAsync(applicationUser);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(applicationUser);
            var user = await _developerService.GetUser(applicationUser.UserId);
            var name = user.Name;
            var card = (await _paymentService.GetBankAccount(user.Id)).Number;
            AvatarPath = (await _fileService.GetAvatar(user.Id, CreatorType.User)).Path;

            Input = new InputModel
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Card = card
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var name = (await _developerService.GetUser(user.UserId)).Name;
            if (Input.Name != name)
            {
                //todo
            }

            var email = await _userManager.GetUserNameAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetUserNameAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set email.";
                    return RedirectToPage();
                }
            }
            
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var card = (await _paymentService.GetBankAccount(user.UserId)).Number;
            if (Input.Card != card)
            {
                //todo
            }

            if (Avatar is {ContentType: "image/jpeg" or "image/png"})
            {
                var avatar = new AvatarModel
                {
                    CreatorId = user.UserId,
                    CreatorType = CreatorType.User,
                    Name = user.UserId + Avatar.FileName
                };
                await _fileService.CreateAvatar(avatar);
            }
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
