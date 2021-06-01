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

        public static string Create(CompanyForm companyForm)
        {
            using var context = new DeveloperDbContext();

            if (context.Company.Select(c => c.Name).Contains(companyForm.Name))
                return "Company with same name already exists";

            var user = context
                .User
                .First(u => u.Id == companyForm.UserId);

            context.Company.Add(new Company(companyForm.Name) {Users = new List<User> {user}});
            context.SaveChanges();
            return string.Empty;
        }
    }
}