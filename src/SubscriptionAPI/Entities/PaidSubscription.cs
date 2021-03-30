using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SubscriptionAPI.Entities
{
    public class PaidSubscription
    {
        public int UserId { get; set; }
        //public Tariff Tariff { get; set; }
        [ForeignKey("Tariff")]
        public int Tariff { get; set; }
        public int SubscribedToId { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAutorenewal {get;set; }
    }
}
