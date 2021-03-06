using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Web.Data;

namespace SupBlog.Web.Configuration
{
    public static class AppService
    {
        public static void AddDefaultServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            /*            serviceCollection.AddControllersWithViews();
                        serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
                        serviceCollection.AddHealthChecks();*/

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                            configuration.GetConnectionString("DefaultConnection"))
                        .UseLazyLoadingProxies())
                .AddIdentity<ApplicationUser, ApplicationRole>(
                    options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            /*            serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<ApplicationDbContext>();*/

            serviceCollection.AddTransient<DbInitializer>();

            serviceCollection.AddControllersWithViews();
            serviceCollection.AddRazorPages();

            serviceCollection.AddAutoMapper(typeof(Startup));
            serviceCollection.AddControllersWithViews();
        }

        private static void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Task<IdentityResult> roleResult;
            var email = "someone@somewhere.com";

            //Check that there is an Administrator role and create if not
            var hasAdminRole = roleManager.RoleExistsAsync("Administrator");
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
                roleResult.Wait();
            }

            //Check if the admin user exists and create it if not
            //Add to the Administrator role

            var testUser = userManager.FindByEmailAsync(email);
            testUser.Wait();

            if (testUser.Result == null)
            {
                var administrator = new ApplicationUser();
                administrator.Email = email;
                administrator.UserName = email;

                var newUser = userManager.CreateAsync(administrator, "_AStrongP@ssword!");
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    var newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
                    newUserRole.Wait();
                }
            }
        }
    }
}