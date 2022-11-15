using DISessionLegacyCore.Models;
using Microsoft.AspNetCore.SystemWebAdapters;

namespace DISessionLegacyCore.Controller;

[Session]
public class BaboonController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IGiveMeBaboon _GiveMeBaboon;

    public BaboonController(IGiveMeBaboon giveMeBaboon)
    {
        _GiveMeBaboon = giveMeBaboon;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new BaboonMe() {Babooned = _GiveMeBaboon.GiveBaboon()});
    }

    [HttpPost]
    public IActionResult Index(BaboonMe pBaboonMe)
    {
        System.Web.HttpContext.Current.Session["Baboon"] = pBaboonMe.Baboon;
        return View(new BaboonMe() {Babooned = _GiveMeBaboon.GiveBaboon()});
    }
}