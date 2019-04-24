using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palestras.Application.Interfaces;
using Palestras.Application.ViewModels;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Core.Notifications;

namespace Palestras.WebApi.Controllers
{
    [Authorize]
    public class PalestranteController : ApiController
    {
        private readonly IPalestranteAppService _palestranteAppService;

        public PalestranteController(
            IPalestranteAppService palestranteAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _palestranteAppService = palestranteAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestrante-management")]
        public IActionResult Get()
        {
            return Response(_palestranteAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestrante-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var palestranteViewModel = _palestranteAppService.GetById(id);

            return Response(palestranteViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWritePalestranteData")]
        [Route("palestrante-management")]
        public IActionResult Post([FromBody] PalestranteViewModel palestranteViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(palestranteViewModel);
            }

            _palestranteAppService.Register(palestranteViewModel);

            return Response(palestranteViewModel);
        }

        [HttpPut]
        [Authorize(Policy = "CanWritePalestranteData")]
        [Route("palestrante-management")]
        public IActionResult Put([FromBody] PalestranteViewModel palestranteViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(palestranteViewModel);
            }

            _palestranteAppService.Update(palestranteViewModel);

            return Response(palestranteViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemovePalestranteData")]
        [Route("palestrante-management")]
        public IActionResult Delete(Guid id)
        {
            _palestranteAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestrante-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var palestranteHistoryData = _palestranteAppService.GetAllHistory(id);
            return Response(palestranteHistoryData);
        }
    }
}