using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TransferModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public int MoneySum { get; set; }
    }
}
