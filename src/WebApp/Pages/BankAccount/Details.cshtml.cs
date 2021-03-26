using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.BankAccount
{
    public class DetailsModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        public DetailsModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public BankAccountModel BankAccountModel { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            BankAccountModel = await _paymentService.GetBankAccount(id);
            return Page();
        }
    }
}
