using System.Collections.Generic;

namespace WebApp.Models
{
    public class CompanyModel : ICreator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}