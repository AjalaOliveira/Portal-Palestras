using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Palestras.Application.Interfaces;
using Palestras.Application.Services;
using Palestras.Domain.CommandHandlers;
using Palestras.Domain.Commands.Palestra;
using Palestras.Domain.Commands.Palestrante;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Core.Events;
using Palestras.Domain.Core.Notifications;
using Palestras.Domain.EventHandlers;
using Palestras.Domain.Events.Palestra;
using Palestras.Domain.Events.Palestrante;
using Palestras.Domain.Interfaces;
using Palestras.Infra.CrossCutting.Bus;
using Palestras.Infra.CrossCutting.Identity.Authorization;
using Palestras.Infra.CrossCutting.Identity.Models;
using Palestras.Infra.CrossCutting.Identity.Services;
using Palestras.Infra.Data.Context;
using Palestras.Infra.Data.EventSourcing;
using Palestras.Infra.Data.Repository;
using Palestras.Infra.Data.Repository.EventSourcing;
using Palestras.Infra.Data.UoW;

namespace Palestras.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<IPalestranteAppService, PalestranteAppService>();
            services.AddScoped<IPalestraAppService, PalestraAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<PalestranteRegisteredEvent>, PalestranteEventHandler>();
            services.AddScoped<INotificationHandler<PalestranteUpdatedEvent>, PalestranteEventHandler>();
            services.AddScoped<INotificationHandler<PalestranteRemovedEvent>, PalestranteEventHandler>();
            services.AddScoped<INotificationHandler<PalestraRegisteredEvent>, PalestraEventHandler>();
            services.AddScoped<INotificationHandler<PalestraUpdatedEvent>, PalestraEventHandler>();
            services.AddScoped<INotificationHandler<PalestraRemovedEvent>, PalestraEventHandler>();


            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewPalestranteCommand>, PalestranteCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePalestranteCommand>, PalestranteCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePalestranteCommand>, PalestranteCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewPalestraCommand>, PalestraCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePalestraCommand>, PalestraCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePalestraCommand>, PalestraCommandHandler>();


            // Infra - Data
            services.AddScoped<IPalestranteRepository, PalestranteRepository>();
            services.AddScoped<IPalestraRepository, PalestraRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<PalestrasDbContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}