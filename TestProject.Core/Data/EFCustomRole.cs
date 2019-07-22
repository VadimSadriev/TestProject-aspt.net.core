using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Core
{
    public class EFCustomRole
    {
        private readonly DataContext context;

        public EFCustomRole(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<CustomRole> Items => context.CustomRoles.Where(x => !x.IsDeleted);


        public async Task<DbResponse<CustomRole>> Add(CustomRoleVm roleVm)
        {
            try
            {
                if (context.CustomRoles.Any(x => x.Name == roleVm.Name))
                {
                    return new DbResponse<CustomRole>
                    {
                        ErrorMessage = "Роль с таким названием уже существует"
                    };
                }

                var customRole = new CustomRole()
                {
                    Name = roleVm.Name,
                    Description = roleVm.Description,

                };

                var appRoles = roleVm.AppRoles.Where(x => x.IsSelected).ToList();

                customRole.AppRoleCustomRoles = roleVm.AppRoles.Where(x => x.IsSelected)
                                                .Select(x => new AppRoleCustomRole
                                                {
                                                    AppRoleId = x.Id,
                                                    CustomRoleId = customRole.Id
                                                }).ToList();
                context.CustomRoles.Add(customRole);

                await context.SaveChangesAsync();

                return new DbResponse<CustomRole>
                {
                    Message = "Роль создана",
                    Response = customRole
                };
            }
            catch (Exception ex)
            {
                // log..
                return new DbResponse<CustomRole>
                {
                    ErrorMessage = "Произошла ошибка при создании роли"
                };
            }
        }

        public async Task<DbResponse<CustomRole>> UpdateAsync(CustomRoleVm customRoleVm)
        {
            try
            {
                var customRole = context.CustomRoles
                                .Include(x => x.AppRoleCustomRoles)
                                .Include(x => x.AppUserCustomRoles)
                                .FirstOrDefault(x => x.Id == customRoleVm.Id);

                if (customRole == null)
                {
                    return new DbResponse<CustomRole>
                    {
                        ErrorMessage = "Роль не существует или была удалена"
                    };


                }

                // main properties
                customRole.Name = customRoleVm.Name;
                customRole.Description = customRoleVm.Description;

                context.ChangeTracker.AutoDetectChangesEnabled = false;

                // approle - customrole
                var newAppRolesIds = customRoleVm.AppRoles.Where(x => x.IsSelected).Select(x => x.Id).ToList();

                var currentAppRolesIds = (from r in context.Roles
                                          join arcr in context.AppRoleCustomRoles
                                          on new { customRoleId = customRole.Id, appRoleId = r.Id }
                                          equals new { customRoleId = arcr.CustomRoleId, appRoleId = arcr.AppRoleId }
                                          select r.Id).ToList();

                var currentCustomRoleAppRoles = customRole.AppRoleCustomRoles.ToDictionary(x => x.AppRoleId);

                foreach (var newAppRoleId in newAppRolesIds)
                {
                    if (!currentAppRolesIds.Contains(newAppRoleId))
                    {
                        customRole.AppRoleCustomRoles.Add(new AppRoleCustomRole { AppRoleId = newAppRoleId, CustomRoleId = customRole.Id });
                    }
                }

                foreach (var currentAppRoleId in currentAppRolesIds)
                {
                    if (!newAppRolesIds.Contains(currentAppRoleId))
                    {
                        customRole.AppRoleCustomRoles.Remove(currentCustomRoleAppRoles[currentAppRoleId]);
                    }
                }

                // update user app roles
                var roleUsersIds = (from u in context.Users
                                    join aucr in context.AppUserCustomRoles
                                    on u.Id equals aucr.AppUserId
                                    where aucr.CustomRoleId == customRole.Id
                                    select u.Id).ToList();

                foreach (var userId in roleUsersIds)
                {
                    var currentUserRolesIds = (from r in context.Roles
                                               join ur in context.UserRoles
                                               on r.Id equals ur.RoleId
                                               where ur.UserId == userId
                                               select r.Id).ToList();

                    foreach (var newAppRoleId in newAppRolesIds)
                    {
                        if (!currentUserRolesIds.Contains(newAppRoleId))
                        {
                            context.UserRoles.Add(new IdentityUserRole<string> { RoleId = newAppRoleId, UserId = userId });
                        }
                    }

                    foreach (var currentUserRoleId in currentAppRolesIds)
                    {
                        if (!newAppRolesIds.Contains(currentUserRoleId))
                        {
                            context.UserRoles.Remove(new IdentityUserRole<string> { RoleId = currentUserRoleId, UserId = userId });
                        }
                    }
                }

                context.ChangeTracker.AutoDetectChangesEnabled = true;

                await context.SaveChangesAsync();

                return new DbResponse<CustomRole>
                {
                    Response = customRole,
                    Message = "Роль изменена"
                };
            }
            catch (Exception ex)
            {
                return new DbResponse<CustomRole>
                {
                    ErrorMessage = "Возника ошибка при обновлении роли"
                };
            }
        }

        public async Task<DbResponse> DeleteAsync(long id)
        {
            try
            {
                var roleToDelete = context.CustomRoles.FirstOrDefault(x => x.Id == id);

                if (roleToDelete == null)
                {
                    return new DbResponse
                    {
                        ErrorMessage = "Роль не существует или была удалена"
                    };
                }

                roleToDelete.IsDeleted = true;

                await context.SaveChangesAsync();

                return new DbResponse
                {
                    Message = "Роль удалена"
                };
            }
            catch (Exception ex)
            {
                return new DbResponse
                {
                    ErrorMessage = "Произошла ошибка во время удалния роли"
                };
            }
        }
    }
}
