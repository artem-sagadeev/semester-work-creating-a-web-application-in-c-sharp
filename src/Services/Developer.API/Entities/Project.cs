using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;

namespace Developer.API.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<User> Users { get; set; }

        public Project(string name)
        {
            Name = name;
        }

        public Project()
        {
        }
    }
}