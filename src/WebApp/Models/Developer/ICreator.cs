using System.Collections.Generic;

namespace WebApp.Models.Developer
{
    public interface ICreator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}