namespace Posts.API.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string FileId { get; set; }
        public string ContentType { get; set; }
        
        public int PostId { get; set; }
        public Post Post { get; set; }

        public File(string fileId, string contentType, int postId)
        {
            FileId = fileId;
            ContentType = contentType;
            PostId = postId;
        }

        public File()
        {
        }
    }
}