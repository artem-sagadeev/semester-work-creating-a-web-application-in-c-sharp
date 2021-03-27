namespace WebApp.Models
{
    public class UserModel : ICreator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}