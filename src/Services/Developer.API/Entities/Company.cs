using System.Collections.Generic;

namespace Developer.API.Entities
{
    public class Company

    {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; }
    public List<Project> Projects { get; set; }
    public List<Tag> Tags { get; set; }
    public Image Image { get; set; }

    public Company(string name)
    {
        Name = name;
    }

    public Company()
    {
    }
    }
}