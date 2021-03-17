using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Developer.API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public List<Project> Projects { get; set; }
        public List<Company> Companies { get; set; }

        public User(string login)
        {
            Login = login;
        }

        public User()
        {
        }
    }
}