namespace TestProject.Core
{
    public class AppUserCustomRole
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public long CustomRoleId { get; set; }
        public virtual CustomRole CustomRole { get; set; }
    }
}
