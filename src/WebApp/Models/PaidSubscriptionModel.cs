using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PaidSubscriptionModel
    {
        public int UserId { get; set; }
        public TariffModel Tariff { get; set; }
        public int SubscribedToId { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAutorenewal { get; set; }
    }
}
