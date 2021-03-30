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

        //BankAccount

        public async Task<IEnumerable<BankAccountModel>> GetBankAccounts()
        {
            var response = await _client.GetAsync($"/Payment/GetBankAccounts");
            return await response.ReadContentAs<IEnumerable<BankAccountModel>>();
        }

        public async Task<BankAccountModel> GetBankAccount(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetBankAccount?userId={userId}");
            return await response.ReadContentAs<BankAccountModel>();
        }


        //Transfers
        public async Task<IEnumerable<TransferModel>> GetTransfers()
        {
            var response = await _client.GetAsync($"/Payment/GetTransfers");
            return await response.ReadContentAs<IEnumerable<TransferModel>>();
        }

        public async Task<IEnumerable<TransferModel>> GetTransfersByUserFrom(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetTransfersByUserFrom?userId={userId}");
            return await response.ReadContentAs<IEnumerable<TransferModel>>();
        }

        public async Task<IEnumerable<TransferModel>> GetTransfersByUserTo(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetTransfersByUserTo?userId={userId}");
            return await response.ReadContentAs<IEnumerable<TransferModel>>();
        }

        //VirtualPurse
        public async Task<IEnumerable<VirtualPurseModel>> GetVirtualPurses()
        {
            var response = await _client.GetAsync($"/Payment/GetVirtualPurses");
            return await response.ReadContentAs<IEnumerable<VirtualPurseModel>>();
        }

        public async Task<VirtualPurseModel> GetVirtualPurse(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetVirtualPurse?userId={userId}");
            return await response.ReadContentAs<VirtualPurseModel>();
        }


        //Withdrawals
        public async Task<IEnumerable<WithdrawalModel>> GetWithdrawals()
        {
            var response = await _client.GetAsync($"/Payment/GetWithdrawals");
            return await response.ReadContentAs<IEnumerable<WithdrawalModel>>();
        }

        //TODO: Не работает GetWithdrawals(int userId);
        public async Task<IEnumerable<WithdrawalModel>> GetWithdrawalsByUserId(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetWithdrawalsByUserId?userId={userId}");
            return await response.ReadContentAs<IEnumerable<WithdrawalModel>>();
        }

    }
}
