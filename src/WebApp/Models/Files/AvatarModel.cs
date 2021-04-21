namespace WebApp.Models.Files
{
    public enum CreatorType
    {
        User,
        Project,
        Company
    }
    
    public class AvatarModel
    {
        public string Id { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; }
        public CreatorType CreatorType { get; set; }
        public string Path => "avatars/" + Name;
    }
}