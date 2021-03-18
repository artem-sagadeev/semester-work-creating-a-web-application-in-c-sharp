using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class Transfer
    {
        public DateTime DateTime { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public int MoneySum { get; set; }
    }
}
