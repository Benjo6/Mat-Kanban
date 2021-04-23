using Mat_Kanban.Data;
using Mat_Kanban.Hubs;
using Mat_Kanban.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddScoped<BoardService>();
            services.AddScoped<CardService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OrganizerAccess", policy => policy.RequireRole("Organizer"));

                options.AddPolicy("TeamPlayerAccess", policy => policy.RequireAssertion(context => context.User.IsInRole("Organizer") || context.User.IsInRole("TeamPlayer")));
                options.AddPolicy("ContributorAccess", policy => policy.RequireAssertion(context => context.User.IsInRole("Organizer") || context.User.IsInRole("TeamPlayer") || context.User.IsInRole("Contributor")));
                options.AddPolicy("ObserverAccess", policy => policy.RequireAssertion(context => context.User.IsInRole("Organizer") || context.User.IsInRole("TeamPlayer") || context.User.IsInRole("Contributor") || context.User.IsInRole("Observer")));


            });

            services.AddAuthentication().AddFacebook(facebookOptions => {
                facebookOptions.AppId = "134381245274013";
                facebookOptions.AppSecret = "50eb90248e40599fd1711b3aa76b4ddb";

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
