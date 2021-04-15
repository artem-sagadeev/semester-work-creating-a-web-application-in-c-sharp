using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
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

    public class TariffModel
    {
        public int Id { get; set; }


        public int PricePerMonth { get; set; }

        public PriceType PriceType { get; set; }

        public TypeOfSubscription TypeOfSubscription { get; set; }

        
    }
}
