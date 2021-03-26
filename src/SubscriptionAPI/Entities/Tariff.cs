using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionAPI.Entities
{
    public class Tariff
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PricePerMounth { get; set; }
        //TODO: Айди (проект, пользователь, компания)
    }
}
