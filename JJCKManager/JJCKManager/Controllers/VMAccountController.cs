using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.Models;
using JJCKManager.BAL;
using JJCKManager.DAL;
using System.Data;

namespace JJCKManager.Controllers
{
    public class VMAccountController : Controller
    {
        private JJCKManagerContext jjckdb = new JJCKManagerContext();
        // GET: VMAccount
        public ActionResult Index()
        {
            IVmAccList vmaccList = new GetAccountList();
            var acclist=vmaccList.GetAllVmAcc();
            return View(acclist);
        }
        public ActionResult AddVmAccount()
        {
            ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName");
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
                    ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName",vMHost.CreateUser);
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