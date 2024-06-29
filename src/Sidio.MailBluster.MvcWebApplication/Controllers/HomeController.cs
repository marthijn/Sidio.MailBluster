using Microsoft.AspNetCore.Mvc;
using Sidio.MailBluster.MvcWebApplication.Models;
using System.Diagnostics;
using Sidio.MailBluster.MvcWebApplication.Services;

namespace Sidio.MailBluster.MvcWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly MailBlusterService _service;
        private readonly ILogger<HomeController> _logger;

        public HomeController(MailBlusterService service, ILogger<HomeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var apiKey = _service.GetApiKey();

            return View(new SetApiKeyModel {ApiKey = apiKey ?? string.Empty});
        }

        [HttpPost]
        public IActionResult SetApiKey(SetApiKeyModel model)
        {
            if (ModelState.IsValid)
            {
                _service.SetApiKey(model.ApiKey);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
