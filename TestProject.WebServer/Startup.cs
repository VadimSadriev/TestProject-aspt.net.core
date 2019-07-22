using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using TestProject.Core;

namespace TestProject.WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            DI.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region custom services

            services.AddDbContext<DataContext>(options =>
            {
                //options.UseLazyLoadingProxies();
                options.UseNpgsql(DI.Configuration["ConnectionStrings:DefaultConnection"]);
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddErrorDescriber<RussianIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services.AddTransient<IRepository, DataRepository>();

            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.AddTransient<ClaimsPrincipal>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/account/login";
                opt.ReturnUrlParameter = "returnUrl";
                opt.Cookie.Name = "auth";
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddSignalR();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            DIContainer.Provider = app.ApplicationServices.CreateScope().ServiceProvider;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Request}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<RequestNotificationHub>("/requestnotification");
            });

            var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            await context.Seed(userManager, roleManager);

        }
    }
}
