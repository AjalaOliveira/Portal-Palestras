using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Core.Notifications;
using Palestras.Infra.CrossCutting.Identity.Models;
using Palestras.Infra.CrossCutting.Identity.Models.AccountViewModels;

namespace Palestras.WebApi.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly ILogger _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            INotificationHandler<DomainNotification> notifications,
            ILoggerFactory loggerFactory,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (!result.Succeeded)
                NotifyError(result.ToString(), "Falha de login");

            _logger.LogInformation(1, "Usuário logado.");
            return Response(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var user = new ApplicationUser {UserName = model.Email, Email = model.Email};

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User claim for write palestrantes data
                await _userManager.AddClaimAsync(user, new Claim("Palestrantes", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Palestrantes", "Remove"));
                await _userManager.AddClaimAsync(user, new Claim("Palestrantes", "Read"));
                await _userManager.AddClaimAsync(user, new Claim("Palestras", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Palestras", "Remove"));
                await _userManager.AddClaimAsync(user, new Claim("Palestras", "Read"));

                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation(3, "O usuário criado com uma nova conta com senha.");
                return Response(model);
            }

            AddIdentityErrors(result);
            return Response(model);
        }
    }
}