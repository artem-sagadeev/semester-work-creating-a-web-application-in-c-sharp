using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IPaymentService
    {
        //BankAccounts
        public Task<IEnumerable<BankAccountModel>> GetBankAccounts();
        public Task<BankAccountModel> GetBankAccount(int userId);
        public Task DeleteBankAccount(int userId);
        public Task AddBankAccount(int number, int userId);

        //Transfers
        public Task<IEnumerable<TransferModel>> GetTransferModels();
        public Task<TransferModel> GetTransferModelByUserFrom(int userId);
        public Task<TransferModel> GetTransferModelByUserTo(int userId);

        public Task AddTransfer(int userFrom, int userTo, int money);

        //VirtualPurses
        public Task<IEnumerable<VirtualPurseModel>> GetVirtualPurses();
        public Task<VirtualPurseModel> GetVirtualPurse(int userId);
        public Task UpdateVirtualPurse(int userId, int money);

        public Task DeleteVirtualPurse(int userId);
        public Task AddVirtualPurse(int userId, int money);

        //Withdrawals
        public Task<IEnumerable<VirtualPurseModel>> GetWithdrawals();
        public Task<IEnumerable<VirtualPurseModel>> GetWithdrawals(int userId);
        public Task AddWithdrawal(int userId, int sum);


    }
}
