using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Core
{
    public static class DbContextExtensions
    {
        public static void ConfigureForNpgSql(this ModelBuilder builder)
        {
            builder.HasPostgresExtension("citext");

            #region one to many

            builder.Entity<AppUser>()
                .HasMany(m => m.Requests)
                .WithOne(o => o.AppUser)
                .HasForeignKey(fk => fk.AppUserId);

            #endregion

            #region many to many

            // AppUser - CustomrRole
            builder.Entity<AppUserCustomRole>()
                .HasKey(k => new { k.AppUserId, k.CustomRoleId });

            builder.Entity<AppUserCustomRole>()
                .HasOne(o => o.AppUser)
                .WithMany(m => m.AppUserCustomRoles)
                .HasForeignKey(fk => fk.AppUserId);

            builder.Entity<AppUserCustomRole>()
                .HasOne(o => o.CustomRole)
                .WithMany(m => m.AppUserCustomRoles)
                .HasForeignKey(fk => fk.CustomRoleId);

            // AppRole - CustomRole
            builder.Entity<AppRoleCustomRole>()
                .HasKey(k => new { k.AppRoleId, k.CustomRoleId });

            builder.Entity<AppRoleCustomRole>()
                .HasOne(o => o.AppRole)
                .WithMany(m => m.AppRoleCustomRoles)
                .HasForeignKey(fk => fk.AppRoleId);

            builder.Entity<AppRoleCustomRole>()
                .HasOne(o => o.CustomRole)
                .WithMany(m => m.AppRoleCustomRoles)
                .HasForeignKey(fk => fk.CustomRoleId);

            #endregion

            #region property settings

            builder.Entity<Request>()
              .Property(r => r.AppUserId)
              .IsRequired();

            #endregion

            #region citext

            builder.Entity<AppUser>()
                .Property(p => p.UserName)
                .HasColumnType("citext");

            builder.Entity<AppUser>()
                .Property(p => p.Email)
                .HasColumnType("citext");


            #endregion
        }

        public static async Task Seed(this DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new AppRole("Admin", "Полный доступ"));
            }

            if (await roleManager.FindByNameAsync("CanApplyRequest") == null)
            {
                await roleManager.CreateAsync(new AppRole("CanApplyRequest", "Подача заявок"));
            }

            if (await roleManager.FindByNameAsync("CanCreateRequest") == null)
            {
                await roleManager.CreateAsync(new AppRole("CanCreateRequest", "Создание заявок"));
            }

            if (await roleManager.FindByNameAsync("CanEditRequest") == null)
            {
                await roleManager.CreateAsync(new AppRole("CanEditRequest", "Редактирование заявок"));
            }

            if (await roleManager.FindByNameAsync("CanViewRequest") == null)
            {
                await roleManager.CreateAsync(new AppRole("CanViewRequest", "Просмотр заявок"));
            }

            if (context.CustomRoles.FirstOrDefault(x => x.Name == "Администратор") == null)
            {
                var adminRole = new CustomRole
                {
                    Name = "Администратор"
                };

                var appRoleAdmind = context.Roles.FirstOrDefault(x => x.Name == "Admin");

                adminRole.AppRoleCustomRoles.Add(new AppRoleCustomRole { AppRoleId = appRoleAdmind.Id, CustomRoleId = adminRole.Id });

                context.CustomRoles.Add(adminRole);

                context.CustomRoles.Add(new CustomRole
                {
                    Name = "Клиент"
                });

                context.SaveChanges();
            }


            if (!context.Users.Any(u => u.Email == "Admin@gmail.com"))
            {
                var adminUser = new AppUser
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com"
                };

                await userManager.CreateAsync(adminUser, "admin12345");

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }


        }
    }
}
