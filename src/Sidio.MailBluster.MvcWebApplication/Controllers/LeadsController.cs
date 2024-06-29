using Microsoft.AspNetCore.Mvc;
using Sidio.MailBluster.MvcWebApplication.Models;
using Sidio.MailBluster.MvcWebApplication.Services;

namespace Sidio.MailBluster.MvcWebApplication.Controllers;

public sealed class LeadsController : Controller
{
    private readonly MailBlusterService _service;

    public LeadsController(MailBlusterService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View(new GetLeadModel
        {
            EmailAddress = string.Empty
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetLead(GetLeadModel model)
    {
        if (ModelState.IsValid)
        {
            var client = _service.CreateClient();
            model.Lead = await client.GetLeadAsync(model.EmailAddress);

            if (model.Lead == null)
            {
                ModelState.AddModelError(string.Empty, "Lead not found.");
            }
        }

        return View(nameof(Index), model);
    }
}