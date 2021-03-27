namespace WebApp.Models
{
    public class CompanyModel : ICreator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}