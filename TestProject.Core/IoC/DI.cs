using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestProject.Core
{
    public static class DI
    {
        public static IConfiguration Configuration { get; set; }

        public static IHostingEnvironment Environment => DIContainer.Provider.GetService<IHostingEnvironment>();

        public static IEmailSender EmailSender => DIContainer.Provider.GetService<IEmailSender>();

        public static IEmailTemplateSender EmailTemplateSender => DIContainer.Provider.GetService<IEmailTemplateSender>();

        public static UserManager<AppUser> UserManager => DIContainer.Provider.GetService<UserManager<AppUser>>();

        public static RoleManager<AppRole> RoleManager => DIContainer.Provider.GetService<RoleManager<AppRole>>();

    }

    public static class DIContainer
    {
        public static IServiceProvider Provider { get; set; }
    }
}
