using System.Collections.Generic;

namespace Posts.API.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string Text { get; set; }
        
        public List<Comment> Comments { get; set; }
        public List<File> Files { get; set; }
    }
}