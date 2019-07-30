using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.BAL;


namespace JJCKManager.Controllers
{
    public class IotTempDataController : Controller
    {
        // GET: IotTempData
        public ActionResult Index()
        {
            IiotDataList iiotData = new GetIotData();           
            return View(iiotData.GetIotList());
        }
    }
}