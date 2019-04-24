using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Palestras.Infra.CrossCutting.Identity.Authorization;
using Palestras.Infra.CrossCutting.Identity.Data;
using Palestras.Infra.CrossCutting.Identity.Models;
using Palestras.Infra.CrossCutting.IoC;
using Palestras.WebApi.Configurations;

namespace Palestras.WebApi
{
    public class StartUpTests
    {
        public StartUpTests(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment()) builder.AddUserSecrets<StartUpTests>();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddWebApi(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
            });

            services.AddAutoMapperSetup();

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
            services.AddMediatR(typeof(StartUpTests));

            // .NET Native DI Abstraction
            RegisterServices(services);

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor accessor)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}