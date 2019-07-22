using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TestProject.Core
{
    public class AppRole : IdentityRole
    {
        public string DisplayName { get; set; }

        public AppRole()
        {
            AppRoleCustomRoles = new List<AppRoleCustomRole>();
        }

        public AppRole(string name) : base(name)
        {
            AppRoleCustomRoles = new List<AppRoleCustomRole>();
        }

        public AppRole(string name, string displayName) : base(name)
        {
            DisplayName = displayName;
            AppRoleCustomRoles = new List<AppRoleCustomRole>();
        }

        public virtual ICollection<AppRoleCustomRole> AppRoleCustomRoles { get; set; }
    }
}
