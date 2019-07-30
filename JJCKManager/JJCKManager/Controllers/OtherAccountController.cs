using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.BAL;
using JJCKManager.Models;

namespace JJCKManager.Controllers
{
    public class OtherAccountController : Controller
    {
        // GET: OtherAccount
        public ActionResult Index()
        {
            IOthAccList othAccList = new GetAccountList();
            return View(othAccList.GetAllOthAcc());
        }
        public ActionResult AddOthAcc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOthAcc(OtherAccount otherAccount)
        {
            IAddOtrherAcc addOtrherAcc = new AddData();
            addOtrherAcc.AddOtAcc(otherAccount);
            return View();
        }
    }
}