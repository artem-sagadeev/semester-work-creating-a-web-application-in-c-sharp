using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionAPI.Entities
{
    public class Tariff
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PricePerMonth { get; set; }

        //[ForeignKey("TypeOfSubscription")]
        public TypeOfSubscription TypeOfSubscription { get; set; }

    }
}
