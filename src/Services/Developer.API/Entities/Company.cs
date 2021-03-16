using System.Collections.Generic;

namespace Developer.API.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; }

        public Company(string name, int ownerId)
        {
            Name = name;
            OwnerId = ownerId;
        }

        public Company()
        {
        }
    }
}