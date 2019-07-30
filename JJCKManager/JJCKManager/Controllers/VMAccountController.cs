using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.Models;
using JJCKManager.BAL;
using System.Data;

namespace JJCKManager.Controllers
{
    public class VMAccountController : Controller
    {
        // GET: VMAccount
        public ActionResult Index()
        {
            IVmAccList vmaccList = new GetAccountList();
            var acclist=vmaccList.GetAllVmAcc();
            return View(acclist);
        }
        public ActionResult AddVmAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVmAccount(VMHostAccount vMHost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IAddVmAcc addVmAcc = new AddData();
                    addVmAcc.AddVmAcc(vMHost);                    
                    return RedirectToAction("Index");
                }
            }
            catch (DataException exc)
            {
                ModelState.AddModelError("", exc);
                
            }
            return View();
        }

    }
}