using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TestProject.Core
{
    public class DataRepository : IRepository
    {
        private DataContext context;
        private AppUser currentUser;
        private string currentUserId;
        private EFAppUser appUsers;
        private EFCustomRole customRoles;
        private EFRequest requests;
        private EFRequestType requestTypes;

        public AppUser CurrentUser
        {
            get
            {
                if (currentUser != null)
                {
                    return currentUser;
                }

                currentUser = context.Users.Find(currentUserId);

                return currentUser;
            }
        }
        public EFCustomRole CustomRoles => customRoles ?? (customRoles = new EFCustomRole(context));
        public EFAppUser AppUsers => appUsers ?? (appUsers = new EFAppUser(context, CurrentUser));
        public EFRequest Requests => requests ?? (requests = new EFRequest(context, CurrentUser));
        public EFRequestType RequestTypes => requestTypes ?? (requestTypes = new EFRequestType(context));

        public DataRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            currentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            this.context = context;
        }
    }
}
