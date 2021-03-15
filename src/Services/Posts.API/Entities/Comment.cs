namespace Posts.API.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        
        public int PostId { get; set; }
        public Post Post { get; set; }

        public Comment(int userId, string text, int postId)
        {
            UserId = userId;
            Text = text;
            PostId = postId;
        }

        public Comment()
        {
        }
    }
}