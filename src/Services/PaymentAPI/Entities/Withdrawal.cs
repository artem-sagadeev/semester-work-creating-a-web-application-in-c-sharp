using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class Withdrawal
    {
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
        public int Sum { get; set; }
    }
}
