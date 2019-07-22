using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TestProject.Core
{
    public class AppUser : IdentityUser
    {
        public bool IsDeleted { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<AppUserCustomRole> AppUserCustomRoles { get; set; }

        public AppUser()
        {
            Requests = new List<Request>();
            AppUserCustomRoles = new List<AppUserCustomRole>();
        }
    }
}
