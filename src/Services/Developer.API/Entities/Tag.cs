using System.Collections.Generic;

namespace Developer.API.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; }
        public List<Company> Companies { get; set; }
    }
}