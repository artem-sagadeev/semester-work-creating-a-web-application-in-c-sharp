namespace WebApp.Models.Subscription
{
    public class TariffModel
    {
        public int Id { get; set; }


        public int PricePerMonth { get; set; }

        public PriceType PriceType { get; set; }

        public TypeOfSubscription TypeOfSubscription { get; set; }

        
    }
}
