using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Developer.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
        public List<Company> Companies { get; set; }
        public List<Tag> Tags { get; set; }
        public Image Image { get; set; }

        public User(string name)
        {
            Name = name;
        }

        public User()
        {
        }
    }
}