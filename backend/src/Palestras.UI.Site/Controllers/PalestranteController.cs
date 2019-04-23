using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Palestras.Application.Interfaces;
using Palestras.Application.ViewModels;
using Palestras.Domain.Core.Notifications;

namespace Palestras.UI.Site.Controllers
{
    [Authorize]
    public class PalestranteController : BaseController
    {
        private readonly IPalestranteAppService _palestranteAppService;
        private readonly IPalestraAppService _palestraAppService;

        public PalestranteController(IPalestranteAppService palestranteAppService,
            IPalestraAppService palestraAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _palestranteAppService = palestranteAppService;
            _palestraAppService = palestraAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestrante-management/list-all")]
        public IActionResult Index()
        {
            return View(_palestranteAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("palestrante-management/palestrante-details/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null) return NotFound();

            var palestranteViewModel = _palestranteAppService.GetById(id.Value);

            if (palestranteViewModel == null) return NotFound();

            palestranteViewModel.Palestras = _palestraAppService.GetPalestrasByPalestranteId(id.Value).ToList();

            return View(palestranteViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWritePalestranteData")]
        [Route("palestrante-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CanWritePalestranteData")]
        [Route("palestrante-management/register-new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PalestranteViewModel palestranteViewModel)
        {
            if (!ModelState.IsValid) return View(palestranteViewModel);
            _palestranteAppService.Register(palestranteViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Palestrante Cadastrado!";

            return View(palestranteViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWritePalestranteData")]
        [Route("palestrante-management/edit-palestrante/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var palestranteViewModel = _palestranteAppService.GetById(id.Value);

            if (palestranteViewModel == null) return NotFound();

            return View(palestranteViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWritePalestranteData")]
        [Route("palestrante-management/edit-palestrante/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PalestranteViewModel palestranteViewModel)
        {
            if (!ModelState.IsValid) return View(palestranteViewModel);

            _palestranteAppService.Update(palestranteViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Palestrante Atualizado!";

            return View(palestranteViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanRemovePalestranteData")]
        [Route("palestrante-management/remove-palestrante/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var palestranteViewModel = _palestranteAppService.GetById(id.Value);

            if (palestranteViewModel == null) return NotFound();

            return View(palestranteViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Policy = "CanRemovePalestranteData")]
        [Route("palestrante-management/remove-palestrante/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _palestranteAppService.Remove(id);

            if (!IsValidOperation()) return View(_palestranteAppService.GetById(id));

            ViewBag.Sucesso = "Palestrante Removido!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("palestrante-management/palestrante-history/{id:guid}")]
        public JsonResult History(Guid id)
        {
            var palestranteHistoryData = _palestranteAppService.GetAllHistory(id);
            return Json(palestranteHistoryData);
        }
    }
}