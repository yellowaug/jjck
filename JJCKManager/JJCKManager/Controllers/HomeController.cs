using JJCKManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace JJCKManager.Controllers
{
    [System.Web.Mvc.AllowAnonymous]//这个页面不用登录验证
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

        // POST: api/Iot
        public int get()
        {
            return 1;
        }

        // POST: api/Iot
        [System.Web.Mvc.HttpPost]
        public int Post([FromBody]IotTemperListFunction iotdata)
        {
            return 1;
        }

    }
}