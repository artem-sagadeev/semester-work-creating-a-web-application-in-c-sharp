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
        public  Task<IEnumerable<BankAccountModel>> GetBankAccounts();
        public  Task<BankAccountModel> GetBankAccount(int userId);


    }
}
