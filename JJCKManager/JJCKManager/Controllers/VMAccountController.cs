using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.Models;
using JJCKManager.BAL;
using JJCKManager.DAL;
using System.Data;
using System.Net;

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
        public ActionResult UpVMacc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            IupVmAccount upvmAcc = new UpToTableData();
            var vmaccs = upvmAcc.UptableVMAcc(id);
            ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName");
            if (vmaccs == null)
            {
                return HttpNotFound();
            }
            return View(vmaccs);

        }
        [HttpPost]
        [ActionName("UpVMacc")]
        [ValidateAntiForgeryToken]
        public ActionResult Putvmacc(int? id, VMHostAccount hostAccount)
        {
            JJCKManagerContext jjckdb = new JJCKManagerContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupOthAccount upothAcc = new UpToTableData();
            var othaccs = upothAcc.UptableOthAcc(id);
            if (TryUpdateModel(upothAcc, "",
               new string[] { "VMhostName", "VMLoginIp", "VMLoginPassWord", "VMCreateTime", "CreateUser" }))
            {
                try
                {
                    ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName", hostAccount.CreateUser);
                    jjckdb.Entry(hostAccount).State = System.Data.Entity.EntityState.Modified;
                    jjckdb.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException dex/* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", dex);
                }
            }
            return View(upothAcc);

        }

    }
}