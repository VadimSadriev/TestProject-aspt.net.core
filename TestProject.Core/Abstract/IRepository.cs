namespace TestProject.Core
{
    public interface IRepository
    {
        EFAppUser AppUsers { get; }
        EFCustomRole CustomRoles { get; }
        AppUser CurrentUser { get; }
        EFRequest Requests { get; }
        EFRequestType RequestTypes { get; }
    }
}
