using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        private class UserIdFormat
        {
            public int userId { get; set; }
        }
        public async Task DeleteBankAccount(int userId)
        {
            await _client.PostAsJsonAsync($"/Payment/DeleteBankAccount", new UserIdFormat()
            {
                userId = userId
            });
        }

        public async Task AddBankAccount(BankAccountModel newBankAccount)
        {
            await _client.PostAsJsonAsync($"/Payment/AddBankAccount", newBankAccount);
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

        public async Task AddTransfer(TransferModel newTransfer)
        {
            await _client.PostAsJsonAsync($"/Payment/AddTransfer", newTransfer);
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

        private class UserIdMoneyIdFormat
        {
            public int userId { get; set; }
            public int money { get; set; }
        }
        public async Task UpdateVirtualPurse(int userId, int money)
        {
            await _client.PostAsJsonAsync($"/Payment/UpdateVirtualPurse", new UserIdMoneyIdFormat(){
                userId = userId,
                    money = money
            });
        }

        public async Task DeleteVirtualPurse(int userId)
        {
            await _client.PostAsJsonAsync($"/Payment/DeleteVirtualPurse", new UserIdFormat(){
                userId = userId,
            });
        }

        public async Task AddVirtualPurse(VirtualPurseModel newVirtualPurse)
        {
            await _client.PostAsJsonAsync($"/Payment/AddVirtualPurse", newVirtualPurse);
        }

        //Withdrawals
        public async Task<IEnumerable<WithdrawalModel>> GetWithdrawals()
        {
            var response = await _client.GetAsync($"/Payment/GetWithdrawals");
            return await response.ReadContentAs<IEnumerable<WithdrawalModel>>();
        }

        
        public async Task<IEnumerable<WithdrawalModel>> GetWithdrawalsByUserId(int userId)
        {
            var response = await _client.GetAsync($"/Payment/GetWithdrawalsByUserId?userId={userId}");
            return await response.ReadContentAs<IEnumerable<WithdrawalModel>>();
        }

        public async Task AddWithdrawal(WithdrawalModel newWithdrawal)
        {
            if (newWithdrawal.ViewOfBankNumber == ViewOfBankNumber.Real)
            {
                //imitation of withdrawal
            }
            else
            {
                
            }
            await _client.PostAsJsonAsync($"/Payment/AddWithdrawal", newWithdrawal);
        }
    }
}
