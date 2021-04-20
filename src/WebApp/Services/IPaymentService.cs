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

        public Task AddBankAccount(BankAccountModel newBankAccount);
        
        //Transfers
        public Task<IEnumerable<TransferModel>> GetTransfers();
        public Task<IEnumerable<TransferModel>> GetTransfersByUserFrom(int userId);
        public Task<IEnumerable<TransferModel>> GetTransfersByUserTo(int userId);

        public Task AddTransfer(TransferModel newTransfer);

        //VirtualPurses
        public Task<IEnumerable<VirtualPurseModel>> GetVirtualPurses();
        public Task<VirtualPurseModel> GetVirtualPurse(int userId);
        public Task UpdateVirtualPurse(int userId, int money);

        public Task DeleteVirtualPurse(int userId);
        public Task AddVirtualPurse(VirtualPurseModel newVirtualPurse);

        //Withdrawals
        public Task<IEnumerable<WithdrawalModel>> GetWithdrawals();
        public Task<IEnumerable<WithdrawalModel>> GetWithdrawalsByUserId(int userId);
        public Task AddWithdrawal(WithdrawalModel newWithdrawal);


    }
}
