namespace TestProject.Core
{
    public class AppRoleVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsSelected { get; set; }

        public AppRoleVm()
        {

        }

        public AppRoleVm(AppRole appRole)
        {
            Id = appRole.Id;
            Name = appRole.Name;
            DisplayName = appRole.DisplayName;
        }
    }
}
