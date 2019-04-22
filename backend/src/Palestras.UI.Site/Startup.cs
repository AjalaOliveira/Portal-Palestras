using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Palestras.Infra.CrossCutting.Identity.Authorization;
using Palestras.Infra.CrossCutting.Identity.Data;
using Palestras.Infra.CrossCutting.Identity.Models;
using Palestras.Infra.CrossCutting.IoC;
using Palestras.UI.Site.Extensions;

namespace Palestras.UI.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString("/login");
                    o.AccessDeniedPath = new PathString("/home/access-denied");
                });
                //.AddFacebook(o =>
                //{
                //    o.AppId = Configuration["Authentication:Facebook:AppId"];
                //    o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                //})
                //.AddGoogle(googleOptions =>
                //{
                //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                //});

            services.AddAutoMapperSetup();
            services.AddMvc();

            //Claims - AddPolicy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWritePalestranteData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestrantes", "Write")));
                options.AddPolicy("CanRemovePalestranteData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestrantes", "Remove")));
                options.AddPolicy("CanWritePalestraData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestras", "Write")));
                options.AddPolicy("CanRemovePalestraData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestras", "Remove")));
            });

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor accessor)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=welcome}/{id?}");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}