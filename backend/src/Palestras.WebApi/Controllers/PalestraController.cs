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
    public class PalestraController : ApiController
    {
        private readonly IPalestraAppService _palestraAppService;

        public PalestraController(
            IPalestraAppService palestraAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _palestraAppService = palestraAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management")]
        public IActionResult Get()
        {
            return Response(_palestraAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var palestraViewModel = _palestraAppService.GetById(id);

            return Response(palestraViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWritePalestraData")]
        [Route("palestra-management")]
        public IActionResult Post([FromBody] PalestraViewModel palestraViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(palestraViewModel);
            }

            _palestraAppService.Register(palestraViewModel);

            return Response(palestraViewModel);
        }

        [HttpPut]
        [Authorize(Policy = "CanWritePalestraData")]
        [Route("palestra-management")]
        public IActionResult Put([FromBody] PalestraViewModel palestraViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(palestraViewModel);
            }

            _palestraAppService.Update(palestraViewModel);

            return Response(palestraViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemovePalestraData")]
        [Route("palestra-management")]
        public IActionResult Delete(Guid id)
        {
            _palestraAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var palestraHistoryData = _palestraAppService.GetAllHistory(id);
            return Response(palestraHistoryData);
        }
    }
}