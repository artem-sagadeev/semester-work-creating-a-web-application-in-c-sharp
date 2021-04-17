using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public enum ViewOfBankNumber
    {
        Virtual,
        Real
    }
    public class Withdrawal
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
        public int Sum { get; set; }
        public ViewOfBankNumber ViewOfBankNumber { get; set; }
    }
}
