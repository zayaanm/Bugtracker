using System;
using System.Collections.Generic;

namespace Bugtracker.Models
{
    public interface IProjectRepo
    {

        Project getProject(int Id);

        Project AddProject(Project Project);

        IEnumerable<Project> GetAllProjects();

        void DeleteProject(Project Project);

        void UpdateProject(Project ProjectChanges);
    }
}
