using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public int MoneySum { get; set; }
    }
}
