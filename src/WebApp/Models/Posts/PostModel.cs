using WebApp.Models.Subscription;

namespace WebApp.Models.Posts
{
    public class PostModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Text { get; set; }
        public PriceType RequiredSubscriptionType { get; set; }
        public string ImageName { get; set; }
    }
}