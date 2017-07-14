using BCPAO.PhotoManager.Data;
using BCPAO.PhotoManager.Filters;
using BCPAO.PhotoManager.Middleware;
using BCPAO.PhotoManager.Models;
using BCPAO.PhotoManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using NLog.Extensions.Logging;

namespace BCPAO.PhotoManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(options => options.User.AllowedUserNameCharacters = null)
                .AddEntityFrameworkStores<DatabaseContext, int>()
                .AddUserStore<UserStore<User, Role, DatabaseContext, int>>()
                .AddRoleStore<RoleStore<Role, DatabaseContext, int>>()
                //.AddRoleManager<RoleManager<Role>>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasPermission", policy => policy.Requirements.Add(new HasPermissionRequirement()));
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();


            services.AddTransient<SearchFilterViewModel, SearchFilterViewModel>();
            services.AddTransient<ValidateMimeMultipartContentFilter, ValidateMimeMultipartContentFilter>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            // Needed for NLog.Web 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DatabaseContext context)
        {
            //loggerFactory.AddNLog();
            //app.AddNLogWeb();
            //env.ConfigureNLog("nlog.config");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseInstaller();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Seed Database
            //DatabaseInitializer.Initialize(context);
      }
    }
}
