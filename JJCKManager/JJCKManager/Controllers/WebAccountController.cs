using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.DAL;
using JJCKManager.BAL;
using JJCKManager.Models;
using System.Data;
using System.Net;

namespace JJCKManager.Controllers
{
    public class WebAccountController : Controller
    {
        JJCKManagerContext jjckdb = new JJCKManagerContext();
        // GET: WebAccount
        public ActionResult Index()
        {
            IwebAccList webaccList = new GetAccountList();
            var a= webaccList.GetAllWebAcc();
            ViewBag.LoginName = User.Identity.Name;
            return View(a);
        }
        public ActionResult AddWebAcc()
        {
            ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName");
            ViewBag.loginname = User.Identity.Name;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWebAcc(WebManagerAccount webacc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName", webacc.CreateUser);
                    IAddWebAcc addWebAcc = new AddData();
                    addWebAcc.AddAccInfo(webacc);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dataexc)
            {

                ModelState.AddModelError("", dataexc);
            }
            //ViewData["CreateUser"] = new SelectList(webaccdb.AccountUser, "Uid", "UserNmae", webacc.CreateUser);

            
            return View(webacc);
        }
        public ActionResult UpWebacc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupWebAccount upwebAcc = new UpToTableData();
            var webaccs = upwebAcc.UptableWebAcc(id);
            ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName");
            if (webaccs == null)
            {
                return HttpNotFound();
            }
            return View(webaccs);

        }
        [HttpPost]
        [ActionName("UpWebacc")]
        [ValidateAntiForgeryToken]
        public ActionResult Putwebacc(int? id, WebManagerAccount managerAccount)
        {
            JJCKManagerContext jjckdb = new JJCKManagerContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IupWebAccount upwebAcc = new UpToTableData();
            var webaccs = upwebAcc.UptableWebAcc(id);
            if (TryUpdateModel(webaccs, "",
               new string[] { "AccountName", "AccountPassWord", "WebUrlORIPaddress", "WebAccountDesc", "CreateTime", "CreateUser" }))
            {
                try
                {
                    ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName", managerAccount.CreateUser);
                    jjckdb.Entry(managerAccount).State = System.Data.Entity.EntityState.Modified;
                    jjckdb.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException dex/* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", dex);
                }
            }
            return View(webaccs);

        }
    }
}