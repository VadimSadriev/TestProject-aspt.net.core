using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestProject.Core
{
    public class EFAppUser
    {
        private readonly DataContext context;
        private readonly AppUser currentUser;
        private readonly UserManager<AppUser> userManager = DI.UserManager;

        public EFAppUser(DataContext context, AppUser currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }

        public IQueryable<AppUser> Items => context.Users.Where(x => !x.IsDeleted);


        public async Task<DbResponse<AppUser>> UpdateAsync(AppUserVm userVm)
        {
            try
            {
                var user = context.Users.Include(x => x.AppUserCustomRoles).FirstOrDefault(a => a.Id == userVm.Id);

                if (user == null)
                {
                    return new DbResponse<AppUser>
                    {
                        ErrorMessage = "Пользователь не существует или был удалён"
                    };
                }

                if (user.Email != userVm.Email)
                {
                    if (context.Users.Any(x => x.Email == userVm.Email))
                    {
                        return new DbResponse<AppUser>
                        {
                            ErrorMessage = "Пользователь с такой почтой уже существует"
                        };
                    }
                }

                if (user.UserName != userVm.UserName)
                {
                    if (context.Users.Any(x => x.UserName == userVm.UserName))
                    {
                        return new DbResponse<AppUser>
                        {
                            ErrorMessage = "Пользователь с таким логином уже существует"
                        };
                    }
                }

                var userName = userVm.UserName.Trim();

                if (Regex.IsMatch(userName, "/s+"))
                {
                    return new DbResponse<AppUser>
                    {
                        ErrorMessage = "Логин не должен содержать пробелов"
                    };
                }

                if (!userVm.Email.IsEmail())
                {
                    return new DbResponse<AppUser>
                    {
                        ErrorMessage = "Не корректный адрес эл. почты"
                    };
                }

                // main properties
                user.UserName = userName;
                user.Email = userVm.Email;

                context.ChangeTracker.AutoDetectChangesEnabled = false;
                // custom roles
                var currentUserRolesIds = (from r in context.CustomRoles
                                           join acr in context.AppUserCustomRoles
                                           on new { roleId = r.Id, userId = user.Id }
                                           equals new { roleId = acr.CustomRoleId, userId = acr.AppUserId }
                                           select r.Id).ToList();

                //var appUserCustomRolesDict = context.AppUserCustomRoles.Where(x => x.AppUserId == user.Id).ToDictionary(x => x.CustomRoleId);
                var appUserCustomRolesDict = user.AppUserCustomRoles.ToDictionary(x => x.CustomRoleId);

                var newCustomRolesIds = userVm.CustomRoles.Where(x => x.IsSelected).Select(x => x.Id).ToList();

                foreach (var newRoleId in newCustomRolesIds)
                {
                    if (!currentUserRolesIds.Contains(newRoleId))
                    {
                        user.AppUserCustomRoles.Add(new AppUserCustomRole { AppUserId = user.Id, CustomRoleId = newRoleId });
                    }
                }

                foreach (var currentRoleId in currentUserRolesIds)
                {
                    if (!newCustomRolesIds.Contains(currentRoleId))
                    {
                        user.AppUserCustomRoles.Remove(appUserCustomRolesDict[currentRoleId]);
                    }
                }

                // app roles 
                var oldAppRolesIds = (from r in context.Roles
                                      join ur in context.UserRoles
                                      on r.Id equals ur.RoleId
                                      where ur.UserId == user.Id
                                      select r.Id).ToList();

                var newAppRolesIds = context.CustomRoles.Include(cr => cr.AppRoleCustomRoles)
                                                        .Where(x => newCustomRolesIds.Contains(x.Id))
                                                        .SelectMany(x => x.AppRoleCustomRoles)
                                                        .Select(x => x.AppRoleId)
                                                        .Distinct()
                                                        .ToList();

                var userRolesDict = (from ur in context.UserRoles
                                     where ur.UserId == user.Id
                                     select ur).ToDictionary(x => x.RoleId);


                foreach (var newAppRoleId in newAppRolesIds)
                {
                    if (!oldAppRolesIds.Contains(newAppRoleId))
                    {
                        context.UserRoles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = newAppRoleId });
                    }
                }

                foreach (var oldAppRoleId in oldAppRolesIds)
                {
                    if (!newAppRolesIds.Contains(oldAppRoleId))
                    {
                        context.UserRoles.Remove(userRolesDict[oldAppRoleId]);
                    }
                }

                context.ChangeTracker.AutoDetectChangesEnabled = true;
                await context.SaveChangesAsync();

                return new DbResponse<AppUser>
                {
                    Response = user,
                    Message = "Пользователь успешно изменён"
                };
            }
            catch (Exception ex)
            {
                return new DbResponse<AppUser>
                {
                    ErrorMessage = "Возникла ошибка при обновлении пользователя"
                };
            }
        }

        public async Task<DbResponse> DeleteAsync(string id)
        {
            try
            {
                var userToDelete = context.Users.FirstOrDefault(x => x.Id == id);

                if (userToDelete == null)
                {
                    return new DbResponse
                    {
                        ErrorMessage = "Пользователь не  существует или был удалён"
                    };
                }

                userToDelete.IsDeleted = true;

                await context.SaveChangesAsync();

                return new DbResponse
                {
                    Message = "Пользователь удалён"
                };
            }
            catch (Exception ex)
            {
                // log...
                return new DbResponse
                {
                    ErrorMessage = "Произошла ошибка при удалении пользователя"
                };
            }
        }

        public async Task<AppUserVm> GetCurrentUserVmWithPermissions()
        {
            try
            {
                var currentRoles = await userManager.GetRolesAsync(currentUser);

                var appUserVm = new AppUserVm(currentUser)
                {
                    IsAdmin = currentRoles.Contains("Admin"),
                    CanEditRequest = currentRoles.Contains("CanEditRequest"),
                };

                return appUserVm;
            }
            catch
            {
                return new AppUserVm();
            }
        }

        public async Task<bool> IsAdmin()
        {
            try
            {
                return await userManager.IsInRoleAsync(currentUser, "Admin");
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CanApplyRequest()
        {
            try
            {
                return await userManager.IsInRoleAsync(currentUser, "CanApplyRequest");
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CanEditRequest()
        {
            try
            {
                return await userManager.IsInRoleAsync(currentUser, "CanEditRequest");
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CanViewRequest()
        {
            try
            {
                return await userManager.IsInRoleAsync(currentUser, "CanViewRequest");
            }
            catch
            {
                return false;
            }
        }
    }
}
