﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JJCKManager.BAL;
using JJCKManager.DAL;
using JJCKManager.Models;


namespace JJCKManager.Controllers
{
    //[Authorize]
    public class OtherAccountController : Controller
    {
        private JJCKManagerContext jjckdb = new JJCKManagerContext();
        // GET: OtherAccount
        public ActionResult Index()
        {
            IAlivedothList alivedothList = new GetAccountList();
            //IOthAccList othAccList = new GetAccountList();

            return View(alivedothList.GetOtherAccountsalived());
        }
        public ActionResult AddOthAcc()
        {
            ViewData["Creater"] = new SelectList(jjckdb.Accounts, "Uid", "UserName");
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult UpOthacc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupOthAccount upothAcc = new UpToTableData();
            var othaccs = upothAcc.UptableOthAcc(id);
            ViewData["Creater"] = new SelectList(jjckdb.Accounts, "Uid", "UserName");
            if (othaccs == null)
            {
                return HttpNotFound();
            }
            return View(othaccs);

        }
        [HttpPost]
        [ActionName("UpOthacc")]
        [ValidateAntiForgeryToken]
        public ActionResult Putothacc(int? id, OtherAccount otherAccount)
        {
            //JJCKManagerContext jjckdb = new JJCKManagerContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupOthAccount upothAcc = new UpToTableData();
            var othaccs = upothAcc.UptableOthAcc(id);
            if (TryUpdateModel(othaccs, "",
               new string[] { "OtherAccountName", "PassWord", "AccountDesc", "Creater", "DaId" }))
            {
                try
                {
                    ViewData["Creater"] = new SelectList(jjckdb.Accounts, "Uid", "UserName", otherAccount.Creater);                    
                    jjckdb.Entry(otherAccount).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult Delacc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupOthAccount upothAcc = new UpToTableData();
            var othaccs = upothAcc.UptableOthAcc(id);
            if (othaccs == null)
            {
                return HttpNotFound();
            }
            return View(othaccs);
        }

        [HttpPost]
        [ActionName("Delacc")]
        [ValidateAntiForgeryToken]
        public ActionResult DelectOthacc(int? id)//软删除
        {
           
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var othaccs = jjckdb.OtherAccounts.Find(id);
            if (othaccs != null)
            { 
                othaccs.DaId = (int)EuDataStatus.isdelete; 
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