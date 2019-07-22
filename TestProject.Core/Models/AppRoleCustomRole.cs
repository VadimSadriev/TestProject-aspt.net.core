namespace TestProject.Core
{
    public class AppRoleCustomRole
    {
        public string AppRoleId { get; set; }
        public virtual AppRole AppRole { get; set; }

        public long CustomRoleId { get; set; }
        public virtual CustomRole CustomRole { get; set; }
    }
}
