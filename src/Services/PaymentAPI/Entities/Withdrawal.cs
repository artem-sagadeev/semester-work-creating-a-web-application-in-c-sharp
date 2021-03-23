using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class Withdrawal
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
        public int Sum { get; set; }
    }
}
