using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projekt_dotnet4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Projekt()
        {
            ViewData["Layout"] = "_Layout2";
            return View();
        }
        
        [Route("~/Silnia/{arg=0}")]
        public ActionResult Silnia(int arg)
        {
            ViewData["Argument"] = arg;

            return View();
        }

        public ActionResult AccessDenied(string c,string a)
        {
            ViewData["Title"] = "Brak dostępu";
            ViewData["Message"] = $"Nie masz dostępu do {c}/{a}";
            return View();
        }
    }
}