using Base36Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Base36Mvc.Controllers;

public class Base36Controller : Controller
{
    [HttpGet]
    public IActionResult Index() => View(new Base36Model());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(Base36Model model)
    {
        if (!ModelState.IsValid)
            return View(model);

        model.Calculate();
        return View(model);
    }
}
