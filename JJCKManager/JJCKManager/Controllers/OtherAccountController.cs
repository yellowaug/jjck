using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.BAL;
using JJCKManager.DAL;
using JJCKManager.Models;


namespace JJCKManager.Controllers
{
    public class OtherAccountController : Controller
    {
        JJCKManagerContext jjckdb = new JJCKManagerContext();
        // GET: OtherAccount
        public ActionResult Index()
        {
            IOthAccList othAccList = new GetAccountList();
            return View(othAccList.GetAllOthAcc());
        }
        public ActionResult AddOthAcc()
        {
            ViewData["Creater"] = new SelectList(jjckdb.Accounts, "Uid", "UserName");
            return View();
        }
        [HttpPost]
        public ActionResult AddOthAcc(OtherAccount otherAccount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewData["Creater"] = new SelectList(jjckdb.Accounts, "Uid", "UserName",otherAccount.Creater);
                    IAddOtrherAcc addOtrherAcc = new AddData();
                    addOtrherAcc.AddOtAcc(otherAccount);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dexc)
            {

                ModelState.AddModelError("", dexc);
            }

            return View();
        }
    }
}