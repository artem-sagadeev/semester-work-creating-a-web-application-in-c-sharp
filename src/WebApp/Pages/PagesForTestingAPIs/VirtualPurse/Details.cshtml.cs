using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.VirtualPurse
{
    public class DetailsModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        public DetailsModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public VirtualPurseModel VirtualPurses { get; set; }

        public async Task<ActionResult> OnGetAsync(int userId)
        {
            VirtualPurses = await _paymentService.GetVirtualPurse(userId);
            return Page();
        }
    }
}
