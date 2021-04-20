using System;

namespace WebApp.Models.Payment
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
