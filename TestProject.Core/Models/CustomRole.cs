using System.Collections.Generic;

namespace TestProject.Core
{
    public class CustomRole : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AppRoleCustomRole> AppRoleCustomRoles { get; set; }
        public virtual ICollection<AppUserCustomRole> AppUserCustomRoles { get; set; }

        public CustomRole()
        {
            AppUserCustomRoles = new List<AppUserCustomRole>();
            AppRoleCustomRoles = new List<AppRoleCustomRole>();
        }
    }
}
