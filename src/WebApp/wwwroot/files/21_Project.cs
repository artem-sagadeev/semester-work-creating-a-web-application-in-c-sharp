using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
using System.Linq;
using System.Linq.Expressions;
using Developer.API.Forms;
using Microsoft.EntityFrameworkCore;

namespace Developer.API.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public List<User> Users { get; set; }
        public List<Tag> Tags { get; set; }

        public Project(string name)
        {
            Name = name;
        }
        
        public static string Create(ProjectForm projectForm)
        {
            using var context = new DeveloperDbContext();

            if (context.Project.Select(p => p.Name).Contains(projectForm.Name))
                return "Project with same name already exists";
            
            if (projectForm.CompanyId == 0)
            {
                var project = new Project(projectForm.Name)
                {
                    Users = new List<User> {context.User.First(u => u.Id == projectForm.UserId)}
                };
                context.Project.Add(project);
                context.SaveChanges();
            }
            else
            {
                var userCompaniesIds = context
                    .User
                    .Where(u => u.Id == projectForm.UserId)
                    .Select(u => u.Companies.Select(c => c.Id))
                    .First();
                if (!userCompaniesIds.Contains(projectForm.CompanyId))
                    return "You don't have access to create projects for this company";
            
                var project = new Project(projectForm.Name)
                {
                    Company = context.Company.First(c => c.Id == projectForm.CompanyId),
                    Users = new List<User> {context.User.First(u => u.Id == projectForm.UserId)}
                };
                context.Project.Add(project);
                context.SaveChanges();
            }

            return string.Empty;
        }

        public Project()
        {
        }
    }
}