using System.Collections.Generic;
using WebApp.Pages;

namespace WebApp.Models.Developer
{
    public class ProjectModel : ICreator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyModel Company { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}