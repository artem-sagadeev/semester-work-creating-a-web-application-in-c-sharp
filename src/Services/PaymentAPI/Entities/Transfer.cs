using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class Transfer
    {
        public DateTime DateTime { get; set; }
        public int User_From { get; set; }
        public int User_To { get; set; }
        public int MoneySum { get; set; }
    }
}
