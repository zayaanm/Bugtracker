using System;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Bugtracker.Security
{
    public class ProjectCheck : IProjectCheck
    {
        private ApplicationDbContext applicationDbContext;

        public ProjectCheck(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;

        }

        public bool Check(int ProjectId, string UserId)
        {


            var userProject = applicationDbContext.UserProjects
                .Where(x => x.ProjectId == ProjectId && x.UserID == UserId)
                .FirstOrDefault();

            if(userProject == null)
            {
                return false;
            }

            return true;

        }
    }
}
