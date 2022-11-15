using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using DISession.Service;
using DISessionLegacy.Models;

namespace DISessionLegacy.Controllers
{

    public class HomeController : Controller
    {
        private readonly IGiveMeBaboon _giveMeBaboon;

        public HomeController()
        {
            
        }

        public HomeController(IGiveMeBaboon pGiveMeBaboon)
        {
            _giveMeBaboon = pGiveMeBaboon;
        }

        [HttpGet]
        public ActionResult Index()
        {
            string baba;
            var baboon = HttpContext.Session["Baboon"];
            if (baboon == null) baba = "Geen baboon";
            else baba = baboon.ToString();
            return View(new BaboonMe() { Baboon = baba, Babooned = _giveMeBaboon.GiveBaboon() });
        }

        [HttpPost]
        public ActionResult Index(BaboonMe pBaboon)
        {
            HttpContext.Session["Baboon"] = pBaboon.Baboon;
            var baboon = HttpContext.Session["Baboon"].ToString();
            return View(new BaboonMe() { Baboon = baboon, Babooned = _giveMeBaboon.GiveBaboon()});
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}