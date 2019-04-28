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
        private readonly IPalestranteAppService _palestranteAppService;

        public PalestraController(
            IPalestraAppService palestraAppService,
            IPalestranteAppService palestranteAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _palestraAppService = palestraAppService;
            _palestranteAppService = palestranteAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management")]
        public IActionResult Get()
        {
            return Response(_palestraAppService.GetAllCompleteList());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var model = new PalestraPalestranteViewModel(_palestraAppService.GetById(id));
            model.Palestrante = _palestranteAppService.GetById(model.PalestranteId);

            return Response(model);
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

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management/search-data/{data:datetime}")]
        public IActionResult SearchByDate(DateTime data)
        {
            var palestraSearchByData = _palestraAppService.SearchByDate(data);
            return Response(palestraSearchByData);
        }
    }
}