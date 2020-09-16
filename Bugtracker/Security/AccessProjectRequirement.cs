using System;
using Microsoft.AspNetCore.Authorization;

namespace Bugtracker.Security
{
    public class AccessProjectRequirement : IAuthorizationRequirement
    {
        public AccessProjectRequirement() 
        {
            
        }

        
    }
}
