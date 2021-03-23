using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI
{
    public class VirtualPurse
    {
        [Key]
        public int UserId { get; set; }

        public int Money { get; set; }
    }
}
