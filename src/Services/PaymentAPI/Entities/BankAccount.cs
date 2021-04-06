using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class BankAccount
    {
        [Key]
        public int UserId { get; set; }
        public int Number { get; set; }
    }
}
