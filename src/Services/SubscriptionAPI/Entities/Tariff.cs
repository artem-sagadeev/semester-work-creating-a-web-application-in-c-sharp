using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionAPI.Entities
{
    public enum PriceType
    {
        Free,
        Basic,
        Improved,
        Max
    }

    public enum TypeOfSubscription
    {
        User,
        Project,
        Team
    }

    public class Tariff
    {
        [Key]
        public int Id { get; set; }

        public int PricePerMonth { get; set; }

        public PriceType PriceType { get; set; }

        public TypeOfSubscription TypeOfSubscription { get; set; }

        //[ForeignKey("TypeOfSubscription")]
        //public TypeOfSubscription TypeOfSubscription { get; set; }
    }
}