using System.Collections.Generic;

namespace WebApp.Models.Developer
{
    public class UserModel : ICreator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjectModel> Projects { get; set; }
        public IEnumerable<CompanyModel> Companies { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}