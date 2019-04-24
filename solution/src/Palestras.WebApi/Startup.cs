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
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment()) builder.AddUserSecrets<Startup>();

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
                options.AddPolicy("CanReadPalestranteData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestrantes", "Read")));
                options.AddPolicy("CanWritePalestraData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestras", "Write")));
                options.AddPolicy("CanRemovePalestraData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestras", "Remove")));
                options.AddPolicy("CanReadPalestraData",
                    policy => policy.Requirements.Add(new ClaimRequirement("Palestras", "Read")));
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Palestras Project",
                    Description = "Palestras API Swagger surface",
                    Contact = new Contact
                    {
                        Name = "Ajala Oliveira",
                        Url = "https://www.linkedin.com/in/ajala-oliveira-85917442/"
                    },
                    License = new License
                    {
                        Name = "GitHub",
                        Url = "https://github.com/AjalaOliveira/Portal-Palestras/blob/master/README.md"
                    }
                });
            });

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);

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

            app.UseSwagger();
            app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "Palestras Project API v1.0"); });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}