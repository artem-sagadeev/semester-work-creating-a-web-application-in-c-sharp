namespace WebApp.Models.Files
{
    public class CoverModel
    {
        public string Id { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Path => "covers/" + Name;
    }
}