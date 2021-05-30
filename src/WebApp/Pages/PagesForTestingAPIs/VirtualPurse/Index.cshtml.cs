using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Identity;
using WebApp.Models.Payment;
using WebApp.Services;
using WebApp.Services.Payment;

namespace WebApp.Pages.VirtualPurse
{
    public class IndexModel : PageModel
    {
        public VirtualPurseModel VirtualPurse { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IEnumerable<VirtualPurseModel> VirtualPursesModels { get; set; }

        public IndexModel(IPaymentService paymentService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _signInManager = signInManager;
            _paymentService = paymentService;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            var userId = (await _userManager.GetUserAsync(User)).UserId;
            var all = await _paymentService.GetVirtualPurses();
            if (_signInManager.IsSignedIn(User))
            {
                //var userId = (await _userManager.GetUserAsync(User)).UserId;
                VirtualPurse = await _paymentService.GetVirtualPurse(userId);
            }

            return Page();
        }

      
    }
}
