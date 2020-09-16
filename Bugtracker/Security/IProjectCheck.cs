using System;
using Bugtracker.Models;

namespace Bugtracker.Security
{
    public interface IProjectCheck
    {
        public bool Check(int ProjectId, string UserId);
    }
}
