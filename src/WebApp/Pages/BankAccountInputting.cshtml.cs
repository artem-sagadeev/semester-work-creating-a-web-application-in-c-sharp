using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models.Identity;
using WebApp.Models.Payment;
using WebApp.Models.Subscription;
using WebApp.Services.Payment;
using Microsoft.AspNetCore.Http.Headers;

namespace WebApp.Pages
{
    public class BankAccountInputtingModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentService _paymentService;
        private static string Referer;
        public BankAccountInputtingModel(UserManager<ApplicationUser> userManager, IPaymentService paymentService)
        {
            _userManager = userManager;
            _paymentService = paymentService;

        }
        public async Task<IActionResult> OnPostAddBankAccountAsync(string number, string refererLink)
        {
            var userId = (await _userManager.GetUserAsync(User)).UserId;


            await _paymentService.AddBankAccount(new BankAccountModel()
            {
                UserId = userId,
                Number = number
            });

            //TODO:œŒ◊≈Ã”  »ƒ¿≈“ »—Àﬁ◊≈Õ»≈?
            //var referer = HttpContext.Session.GetString("Referer");
            //HttpContext.Session.Remove("Referer");
            //string referer = Request.Headers["Referer"].ToString();

            //Response.Redirect(refererLink);
            return Redirect(refererLink);
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = (await _userManager.GetUserAsync(User)).UserId;
            RequestHeaders header = Request.GetTypedHeaders();
            Uri uriReferer = header.Referer;
            if (uriReferer == null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Referer"] = uriReferer.ToString();
            }

            //if (!HttpContext.Session.Keys.Contains("Referer"))
            //{
            //    RequestHeaders header = Request.GetTypedHeaders();
            //    Uri uriReferer = header.Referer;
            //    if (uriReferer == null)
            //    {
            //        return RedirectToPage("Index");
            //    }
            //    else
            //    {
            //          HttpContext.Session.SetString("Referer", uriReferer.ToString());
            //    }

            //}
            //if (await _paymentService.GetBankAccount(userId) != null)
            //{
            //    var referer = HttpContext.Session.GetString("Referer");
            //    HttpContext.Session.Remove("Referer");
            //    Response.Redirect(referer);
            //}

            return Page();
        }
    }
}
