using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Bugtracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bugtracker.Security
{
    public class CanAccessProjectHandler : AuthorizationHandler<AccessProjectRequirement, Project> 
    {

        private readonly IProjectCheck projectCheck;

        public CanAccessProjectHandler(IProjectCheck _projectCheck)
        {
            projectCheck = _projectCheck;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessProjectRequirement requirement, Project project)
        {

            string userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isValid = projectCheck.Check(project.ProjectId, userId);

            if (isValid)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
