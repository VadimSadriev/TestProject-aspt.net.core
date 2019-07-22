using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Core
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<CustomRole> CustomRoles { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestTypeField> RequestTypeFields { get; set; }
        public DbSet<RequestValue> RequestValues { get; set; }
        public DbSet<AppRoleCustomRole> AppRoleCustomRoles { get; set; }
        public DbSet<AppUserCustomRole> AppUserCustomRoles { get; set; }

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureForNpgSql();
        }
    }
}
