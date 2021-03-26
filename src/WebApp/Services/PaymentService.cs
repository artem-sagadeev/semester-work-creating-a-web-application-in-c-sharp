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

        public async Task DeleteBankAccount(int userId)
        {
            throw new NotImplementedException();
            //var responce = await _client.GetAsync($"/Payment/DeleteBankAccount");
            //return;
        }

        public async Task AddBankAccount(int userId, int num)
        {
            throw new NotImplementedException();
            //var responce = await _client.GetAsync($"/Payment/AddBankAccount");
        }


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

        public Task AddTransfer(int userFrom, int userTo, int money)
        {
            throw new NotImplementedException();
        }

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

        public Task UpdateVirtualPurse(int userId, int money)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVirtualPurse(int userId)
        {
            throw new NotImplementedException();
        }

        public Task AddVirtualPurse(int userId, int money)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WithdrawalModel>> GetWithdrawals()
        {
            var response = await _client.GetAsync($"/Payment/GetWithdrawals");
            return await response.ReadContentAs<IEnumerable<WithdrawalModel>>();
        }

        public async Task<IEnumerable<WithdrawalModel>> GetWithdrawals(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetWithdrawals?userId={userId}");
            return await response.ReadContentAs<IEnumerable<WithdrawalModel>>();
        }

        public Task AddWithdrawal(int userId, int sum)
        {
            throw new NotImplementedException();
        }



    }
}
