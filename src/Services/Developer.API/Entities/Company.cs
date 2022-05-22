using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Developer.API.Forms;

namespace Developer.API.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; }
        public List<Tag> Tags { get; set; }

        public Company(string name)
        {
            Name = name;
        }

        public Company()
        {
        }
    }
}