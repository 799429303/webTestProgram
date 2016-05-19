using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        [CheckinLogin]  
        public ActionResult Index()
        {
            return View("MainPage");
        }

        public string GetString()
        {
            return "hello world";
        }

    }
}