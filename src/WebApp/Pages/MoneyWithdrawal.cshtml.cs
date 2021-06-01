using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Identity;
using WebApp.Models.Payment;
using WebApp.Services.Payment;

namespace WebApp.Pages
{
    public class MoneyWithdrawalModel : PageModel
    {
        public VirtualPurseModel VirtualPurse { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MoneyWithdrawalModel(UserManager<ApplicationUser> userManager, IPaymentService paymentService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _paymentService = paymentService;
            _signInManager = signInManager;
        }
        public async void OnGetAsync()
        {
            if (!_signInManager.IsSignedIn(User)) return;
            var userId = (await _userManager.GetUserAsync(User)).UserId;
            VirtualPurse = await _paymentService.GetVirtualPurse(userId);
        }

        public async Task<IActionResult> OnPostAsync(int money)
        {
            var userId = (await _userManager.GetUserAsync(User)).UserId;
            if (money <= (await _paymentService.GetVirtualPurse(userId)).Money)
            {
                await _paymentService.AddWithdrawal(new WithdrawalModel()
                {
                    DateTime = DateTime.Now,
                    Sum = money,
                    UserID = userId,
                    ViewOfBankNumber = ViewOfBankNumber.Virtual
                });
                await _paymentService.TransferMoneyToBankAccount(await _paymentService.GetBankAccount(userId));
                await _paymentService.UpdateVirtualPurse(userId, (await _paymentService.GetVirtualPurse(userId)).Money - money);
            }
            return RedirectToPage("MoneyWithdrawal");
        }
    }
}
