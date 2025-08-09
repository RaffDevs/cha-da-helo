using System.Threading.Tasks;
using ChaHelo.Application.Models;
using ChaHelo.Application.Usecases;
using ChaHelo.Infra.Repositories;
using ChaHelo.Webapp.Enums;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace ChaHelo.Webapp.Controllers
{
    public class PresenceController : Controller
    {
        private readonly IConfiguration _configuration;

        public PresenceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(
            ConfirmPresenceInputModel model,
            [FromServices] ConfirmPresenceUseCase useCase
        )
        {
            if (!ModelState.IsValid)
            {
                TempData["ToastMessage"] = "Oops... acho que algo deu errado!";
                TempData["ToastType"] = ToastType.Error.Humanize();
                TempData["ToastLabel"] = "Erro";

                return View(model);
            }

            var result = await useCase.Call(model.FullName);

            if (!result.IsSuccess)
            {
                TempData["ToastMessage"] = "Oops... ocorreu um erro ao confirmar sua presença...!";
                TempData["ToastType"] = ToastType.Error.Humanize();
                TempData["ToastLabel"] = "Erro";

                return View(model);
            }

            TempData["ToastMessage"] = "Obrigada por confirmar sua presença no chá da Heloísa! 💕";
            TempData["ToastType"] = ToastType.Success.Humanize();
            TempData["ToastLabel"] = "Presença Confirmada!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/lista")]
        public async Task<IActionResult> List([FromQuery] string key, [FromServices] ListPresencesUseCase useCase)
        {
            if (key != _configuration["admin:secret"])
            {
                return Unauthorized();
            }

            var result = await useCase.Call();

            if (!result.IsSuccess)
            {
                return View(result.exception.Message);
            }

            return View(result.Value);
        }

    }
}
