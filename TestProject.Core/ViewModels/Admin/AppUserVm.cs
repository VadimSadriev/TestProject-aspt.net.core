using System.Collections.Generic;

namespace TestProject.Core
{
    public class AppUserVm
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool CanEditRequest { get; set; }
        public bool CanApplyRequest { get; set; }

        public List<CustomRoleVm> CustomRoles { get; set; }

        public AppUserVm()
        {
            CustomRoles = new List<CustomRoleVm>();
        }

        public AppUserVm(AppUser appUser)
        {
            Id = appUser.Id;
            UserName = appUser.UserName;
            Email = appUser.Email;
            CustomRoles = new List<CustomRoleVm>();
        }
    }
}
