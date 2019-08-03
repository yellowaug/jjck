using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.Models;
using JJCKManager.BAL;
using System.Data;
using JJCKManager.DAL;
using System.Net;

namespace JJCKManager.Controllers
{
    [Authorize]
    public class SystemAccountController : Controller
    {
        private JJCKManagerContext jjckdb = new JJCKManagerContext();
        // GET: SystemAccount
        public ActionResult Index()
        {
            ViewBag.loginname = User.Identity.Name;//显示登录账号
            //IAccList accList = new GetAccountList();
            IAlivedSysAccList alivedSysAccList = new GetAccountList();
            var alivedsysresult = alivedSysAccList.GetAccountsalived();
            return View(alivedsysresult);
            
        }
        public ActionResult AddAcc()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAcc(Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IAddData ia = new AddData();
                    ia.addAccount(account);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dataex)
            {

                ModelState.AddModelError("", dataex);
            }
           
            return View(account);
        }
        //[HttpPut]
        public ActionResult UpSysacc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupAccount upsysacc = new UpToTableData();
            var sysaccres = upsysacc.UptableSysAcc(id);
            if ( sysaccres== null)
            {
                return HttpNotFound();
            }
            return View(sysaccres);
        }
        [HttpPost]
        [ActionName("UpSysacc")]
        [ValidateAntiForgeryToken]
        public ActionResult PutUsSysacc(int? id,Account account)
        {
            JJCKManagerContext jjckdb = new JJCKManagerContext();
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupAccount upsysacc = new UpToTableData();
            var sysaccres = upsysacc.UptableSysAcc(id);
            if (TryUpdateModel(sysaccres, "",
               new string[] { "UserName", "PassWord", "Createdate" }))
            {
                try
                {
                    jjckdb.Entry(account).State= System.Data.Entity.EntityState.Modified;
                    jjckdb.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException dex/* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", dex);
                }
            }
            return View(sysaccres);

        }
        public ActionResult DeleteSysacc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupAccount upsysacc = new UpToTableData();
            var sysaccres = upsysacc.UptableSysAcc(id);
            if (sysaccres == null)
            {
                return HttpNotFound();
            }
            return View(sysaccres);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteSysacc")]
        public ActionResult DeSysacc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sysaccresult = jjckdb.Accounts.Find(id);
            if (sysaccresult!=null)
            {
                sysaccresult.DaId = (int)EuDataStatus.isdelete;
                jjckdb.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
            
        }

    }
}