using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;

namespace Developer.API.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
        
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<User> Users { get; set; }

        public Project(string name, int ownerId)
        {
            Name = name;
            OwnerId = ownerId;
        }

        public Project()
        {
        }
    }
}