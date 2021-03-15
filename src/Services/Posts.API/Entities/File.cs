namespace Posts.API.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string FileId { get; set; }
        public string ContentType { get; set; }
        
        public Post Post { get; set; }
    }
}