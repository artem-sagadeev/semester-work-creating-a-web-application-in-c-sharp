using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _client;

        public PaymentService(HttpClient client)
        {
            _client = client;
        }


        public async Task<BankAccountModel> GetBankAccount(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetBankAccount?userId={userId}");
            return await response.ReadContentAs<BankAccountModel>();
        }

        public async Task<IEnumerable<BankAccountModel>> GetBankAccounts()
        {
            var response = await _client.GetAsync($"/Payment/GetBankAccounts");
            return await response.ReadContentAs<IEnumerable<BankAccountModel>>();
        }

    }
}
