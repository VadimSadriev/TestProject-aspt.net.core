using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Core;

namespace TestProject.WebServer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IRepository repository;

        public AdminController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IRepository repository
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.repository = repository;
        }

        #region public methods

        public IActionResult Index()
        {
            ViewData["Title"] = "Панель администратора";
            return View();
        }



        #region users 

        [HttpGet]
        public async Task<ApiResponse> GetUsers()
        {
            var items = await repository.AppUsers.Items.Where(x => !x.IsDeleted).Include(u => u.AppUserCustomRoles)
                              .ThenInclude(apcr => apcr.CustomRole)
                              .OrderBy(x => x.UserName)
                              .Select(u => new { user = new AppUserVm(u), roles = u.AppUserCustomRoles.Select(x => x.CustomRole.Id) })
                              .ToListAsync();

            var allRoles = repository.CustomRoles.Items.ToList();

            foreach (var item in items)
            {
                item.user.CustomRoles.AddRange(allRoles.Select(r => new CustomRoleVm
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IsSelected = item.roles.Contains(r.Id)
                }).OrderBy(x => x.Name));
            }

            return new ApiResponse
            {
                Response = new
                {
                    users = items.Select(x => x.user).ToList(),
                    roles = allRoles.Select(x => new CustomRoleVm(x))
                }
            };
        }

        [HttpPost]
        public async Task<ApiResponse<AppUserVm>> AddUser([FromBody]AppUserVm userCredentials)
        {
            var defaultErrorResponse = new ApiResponse<AppUserVm>
            {
                ErrorMessage = "Пожалуйста, предоставьте все необходимые данные для создания учетной записи пользователя"
            };

            if (userCredentials == null)
                return defaultErrorResponse;

            if (string.IsNullOrWhiteSpace(userCredentials.UserName))
                return defaultErrorResponse;

            if (repository.AppUsers.Items.Any(userEmail => userEmail.Email == userCredentials.Email)
                || repository.AppUsers.Items.Any(userLogin => userLogin.UserName == userCredentials.UserName))
            {
                return new ApiResponse<AppUserVm>
                {
                    ErrorMessage = "Пользователь с данной эл. почтой или логином уже существует"
                };
            }

            if (!userCredentials.Email.IsEmail())
            {
                return new ApiResponse<AppUserVm>
                {
                    ErrorMessage = "Не корректная эл. почта"
                };
            }

            var user = new AppUser
            {
                UserName = userCredentials.UserName.Trim(),
                Email = userCredentials.Email.Trim()
            };

            user.AppUserCustomRoles = userCredentials.CustomRoles.Where(x => x.IsSelected).Select(cr => new AppUserCustomRole
            {
                CustomRoleId = cr.Id,
                AppUserId = user.Id
            }).ToList();

            var randomPassword = RandomPasswordGenerator.GenerateRandomPassword();

            var result = await userManager.CreateAsync(user, randomPassword);

            if (result.Succeeded)
            {
                TestProjectEmailSender.SendAccountPasswordEmailAsync(user.Email, randomPassword);

                var customRolesIds = userCredentials.CustomRoles.Where(x => x.IsSelected).Select(r => r.Id).ToList();

                var appRolesToAdd = repository.CustomRoles.Items.Where(cr => customRolesIds.Contains(cr.Id))
                        .SelectMany(cRole => cRole.AppRoleCustomRoles).Select(x => x.AppRole.Name).ToList();

                var rolesResult = await userManager.AddToRolesAsync(user, appRolesToAdd);

                if (rolesResult.Succeeded)
                {
                    var allRoles = repository.CustomRoles.Items.ToList();

                    var userVm = new AppUserVm(user);

                    userVm.CustomRoles.AddRange(allRoles.Select(r => new CustomRoleVm
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        IsSelected = customRolesIds.Contains(r.Id)
                    }).OrderBy(x => x.Name));

                    return new ApiResponse<AppUserVm>
                    {
                        Response = userVm,
                        Message = $"Пользователь успешно создан, пароль от учётной записи будет выслан на почту {userVm.Email} \n ... pass: {randomPassword}"

                    };
                }

                return new ApiResponse<AppUserVm>
                {
                    ErrorMessage = "Произошла ошибка при обработке вашего запроса"
                };
            }

            return new ApiResponse<AppUserVm>
            {
                ErrorMessage = result.Errors.AggregateErrors()
            };
        }

        [HttpPost]
        public async Task<ApiResponse<AppUserVm>> UpdateUser([FromBody]AppUserVm userCredentials)
        {
            var dbResponse = await repository.AppUsers.UpdateAsync(userCredentials);

            if (dbResponse.Success)
            {
                var userVm = new AppUserVm(dbResponse.Response);

                var userRolesIds = repository.CustomRoles.Items.Select(x => x.Id);

                var allRoles = repository.CustomRoles.Items.ToList();

                var userCustomRoles = dbResponse.Response.AppUserCustomRoles.Select(x => x.CustomRole.Id);

                userVm.CustomRoles.AddRange(allRoles.Select(r => new CustomRoleVm
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IsSelected = userCustomRoles.Contains(r.Id)
                }).OrderBy(x => x.Name));

                return new ApiResponse<AppUserVm>
                {
                    Response = userVm,
                    Message = dbResponse.Message
                };
            }

            return new ApiResponse<AppUserVm>
            {
                ErrorMessage = "Произошла ошибка при обработке вашего запроса"
            };
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteUser([FromBody]AppUserVm appUserVm)
        {
            if (repository.CurrentUser?.Id == appUserVm.Id)
            {
                return new ApiResponse
                {
                    ErrorMessage = "Невозможно удалить пользователя"
                };
            }

            var dbResponse = await repository.AppUsers.DeleteAsync(appUserVm.Id);

            if (dbResponse.Success)
            {
                return new ApiResponse
                {
                    Message = dbResponse.Message
                };
            }

            return new ApiResponse
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }

        #endregion


        #region roles

        [HttpGet]
        public async Task<ApiResponse> GetRoles()
        {
            var result = new List<CustomRoleVm>();
            var appRoles = roleManager.Roles.ToList();
            var items = await repository.CustomRoles.Items.Include(r => r.AppRoleCustomRoles).ThenInclude(x => x.AppRole)
                       .OrderBy(x => x.Name)
                       .Select(x => new { role = new CustomRoleVm(x), appRoles = x.AppRoleCustomRoles.Select(z => z.AppRoleId) })
                       .ToListAsync();

            foreach (var item in items)
            {
                item.role.AppRoles.AddRange(appRoles.Select(x => new AppRoleVm
                {
                    Id = x.Id,
                    DisplayName = x.DisplayName,
                    IsSelected = item.appRoles.Contains(x.Id)
                }).OrderBy(x => x.DisplayName));
            }

            return new ApiResponse
            {
                Response = new
                {
                    roles = items.Select(x => x.role),
                    appRoles = appRoles.Select(x => new AppRoleVm(x))
                }
            };

        }

        [HttpPost]
        public async Task<ApiResponse<CustomRoleVm>> AddRole([FromBody]CustomRoleVm customRoleVm)
        {
            var dbResult = await repository.CustomRoles.Add(customRoleVm);

            if (dbResult.Success)
            {
                var customRole = new CustomRoleVm(dbResult.Response);

                var appRolesIds = customRoleVm.AppRoles.Where(x => x.IsSelected).Select(x => x.Id).ToList();

                var allAppRoles = roleManager.Roles.ToList();

                customRole.AppRoles.AddRange(allAppRoles.Select(x => new AppRoleVm
                {
                    Id = x.Id,
                    DisplayName = x.DisplayName,
                    IsSelected = appRolesIds.Contains(x.Id)
                }).OrderBy(x => x.DisplayName));

                return new ApiResponse<CustomRoleVm>
                {
                    Message = dbResult.Message,
                    Response = customRole
                };
            }

            return new ApiResponse<CustomRoleVm>
            {
                ErrorMessage = dbResult.ErrorMessage
            };
        }

        [HttpPost]
        public async Task<ApiResponse<CustomRoleVm>> UpdateRole([FromBody]CustomRoleVm customRoleVm)
        {
            var dbResponse = await repository.CustomRoles.UpdateAsync(customRoleVm);

            if (dbResponse.Success)
            {
                var roleVm = new CustomRoleVm(dbResponse.Response);

                var allAppRoles = roleManager.Roles.ToList();

                var roleAppRolesIds = customRoleVm.AppRoles.Where(x => x.IsSelected).Select(x => x.Id).ToList();

                roleVm.AppRoles.AddRange(allAppRoles.Select(x => new AppRoleVm
                {
                    Id = x.Id,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    IsSelected = roleAppRolesIds.Contains(x.Id)
                }).OrderBy(x => x.DisplayName));

                return new ApiResponse<CustomRoleVm>
                {
                    Response = roleVm,
                    Message = dbResponse.Message
                };
            }

            return new ApiResponse<CustomRoleVm>
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteRole(long id)
        {
            var dbResponse = await repository.CustomRoles.DeleteAsync(id);

            if (dbResponse.Success)
            {
                return new ApiResponse
                {
                    Message = dbResponse.Message
                };
            }

            return new ApiResponse
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }

        #endregion

        #region request-types

        [HttpGet]
        public async Task<ApiResponse> GetRequestTypes()
        {
            var requestTypes = await repository.RequestTypes.Items.Include(x => x.RequestTypeFields).ToListAsync();

            var reqTypeListVm = requestTypes.Select(x => new RequestTypeVm(x));

            return new ApiResponse
            {
                Response = new
                {
                    RequestTypes = reqTypeListVm,
                    RequestFieldTypes = Enum.GetValues(typeof(RequestFieldType)).Cast<RequestFieldType>().Select(x => new { value = x.ToString(), displayName = x.GetRuName() })
                }
            };
        }

        [HttpPost]
        public async Task<ApiResponse<RequestTypeVm>> AddRequestType([FromBody]RequestTypeVm requestTypeVm)
        {
            var dbResponse = await repository.RequestTypes.AddAsync(requestTypeVm);

            if (dbResponse.Success)
            {
                return new ApiResponse<RequestTypeVm>
                {
                    Message = dbResponse.Message,
                    Response = new RequestTypeVm(dbResponse.Response)
                };
            }

            return new ApiResponse<RequestTypeVm>
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }

        [HttpPost]
        public async Task<ApiResponse<RequestTypeVm>> UpdateRequestType([FromBody]RequestTypeVm requestTypeVm)
        {
            var dbResponse = await repository.RequestTypes.UpdateAsync(requestTypeVm);

            if (dbResponse.Success)
            {
                var reqTypeVm = new RequestTypeVm(dbResponse.Response);

                return new ApiResponse<RequestTypeVm>
                {
                    Message = dbResponse.Message,
                    Response = reqTypeVm
                };
            }

            return new ApiResponse<RequestTypeVm>
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteRequestType(long id)
        {
            var dbResponse = await repository.RequestTypes.DeleteAsync(id);

            if (dbResponse.Success)
            {
                return new ApiResponse
                {
                    Message = dbResponse.Message
                };
            }

            return new ApiResponse
            {
                ErrorMessage = dbResponse.ErrorMessage
            };
        }

        #endregion

        #endregion


        #region private helpers

       
        #endregion
    }
}
