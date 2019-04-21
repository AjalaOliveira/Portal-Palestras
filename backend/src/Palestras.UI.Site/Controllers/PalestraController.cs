using System;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palestras.Application.Interfaces;
using Palestras.Application.ViewModels;
using Palestras.Domain.Core.Notifications;
using Palestras.Domain.Interfaces;

namespace Palestras.UI.Site.Controllers
{
    [Authorize]
    public class PalestraController : BaseController
    {
        private readonly IPalestraAppService _palestraAppService;

        public PalestraController(IPalestraAppService palestraAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _palestraAppService = palestraAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management/list-all")]
        public IActionResult Index()
        {
            return View(_palestraAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestra-management/palestra-details/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();

            var palestraViewModel = _palestraAppService.GetById(id.Value);

            if (palestraViewModel == null) return NotFound();

            return View(palestraViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWritePalestraData")]
        [Route("palestra-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CanWritePalestraData")]
        [Route("palestra-management/register-new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PalestraViewModel palestraViewModel)
        {
            if (!ModelState.IsValid) return View(palestraViewModel);
            _palestraAppService.Register(palestraViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Palestra Cadastrada!";

            return View(palestraViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWritePalestraData")]
        [Route("palestra-management/edit-palestra/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var palestraViewModel = _palestraAppService.GetById(id.Value);

            if (palestraViewModel == null) return NotFound();

            return View(palestraViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWritePalestraData")]
        [Route("palestra-management/edit-palestra/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PalestraViewModel palestraViewModel)
        {
            if (!ModelState.IsValid) return View(palestraViewModel);

            _palestraAppService.Update(palestraViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Palestra Atualizada!";

            return View(palestraViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanRemovePalestraData")]
        [Route("palestra-management/remove-palestra/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var palestraViewModel = _palestraAppService.GetById(id.Value);

            if (palestraViewModel == null) return NotFound();

            return View(palestraViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Policy = "CanRemovePalestraData")]
        [Route("palestra-management/remove-palestra/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _palestraAppService.Remove(id);

            if (!IsValidOperation()) return View(_palestraAppService.GetById(id));

            ViewBag.Sucesso = "Palestra Removida!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("palestra-management/palestra-history/{id:guid}")]
        public JsonResult History(Guid id)
        {
            var palestraHistoryData = _palestraAppService.GetAllHistory(id);
            return Json(palestraHistoryData);
        }
    }
}