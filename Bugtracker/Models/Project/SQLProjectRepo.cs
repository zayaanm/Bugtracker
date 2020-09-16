using System;
using System.Collections.Generic;
using System.Linq;
using Bugtracker.Data;

namespace Bugtracker.Models
{
    public class SQLProjectRepo : IProjectRepo
    {

        public readonly ApplicationDbContext applicationDbContext;

        public SQLProjectRepo(ApplicationDbContext _applicationDbContext)
        {

            applicationDbContext = _applicationDbContext;
        }

        public Project AddProject(Project Project)
        {
            applicationDbContext.Projects.Add(Project);
            applicationDbContext.SaveChanges();
            return Project;
        }

        public void DeleteProject(Project Project)
        {
            applicationDbContext.Projects.Remove(Project);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return applicationDbContext.Projects.ToList();
        }

        public Project getProject(int Id)
        {
            return applicationDbContext.Projects.FirstOrDefault(x => x.ProjectId == Id);
        }

        public void UpdateProject(Project ProjectChanges)
        {
            applicationDbContext.Update(ProjectChanges);
        }
    }
}
