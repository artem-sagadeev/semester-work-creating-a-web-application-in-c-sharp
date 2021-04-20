using System;

namespace WebApp.Models.Payment
{
    public enum ViewOfBankNumber
    {
        Virtual,
        Real
    }
    public class WithdrawalModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
        public int Sum { get; set; }
        public ViewOfBankNumber ViewOfBankNumber { get; set; }
    }
}
