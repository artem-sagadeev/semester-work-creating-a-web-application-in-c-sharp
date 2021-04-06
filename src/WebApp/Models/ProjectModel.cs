using System.Collections.Generic;
using System.Dynamic;

namespace WebApp.Models
{
    public class ProjectModel : ICreator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}