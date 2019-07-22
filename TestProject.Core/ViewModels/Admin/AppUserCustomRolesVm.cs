using System.Collections.Generic;

namespace TestProject.Core
{
    public class CustomRoleVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
        
        public List<AppRoleVm> AppRoles { get; set; }

        public CustomRoleVm()
        {
            AppRoles = new List<AppRoleVm>();
        }

        public CustomRoleVm(CustomRole customRole)
        {
            Id = customRole.Id;
            Name = customRole.Name;
            Description = customRole.Description;
            AppRoles = new List<AppRoleVm>();
        }
    }
}
