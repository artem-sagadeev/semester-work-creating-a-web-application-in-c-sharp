using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TariffModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PricePerMonth { get; set; }

        public TypeOfSubscriptionModel TypeOfSubscription { get; set; }
    }
}
