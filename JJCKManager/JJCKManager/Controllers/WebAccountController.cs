using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.DAL;
using JJCKManager.BAL;
using JJCKManager.Models;

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
            //ViewData["CreateUser"] = new SelectList(webaccdb.AccountUser, "Uid", "UserNmae", webacc.CreateUser);
            ViewData["CreateUser"] = new SelectList(jjckdb.Accounts, "Uid", "UserName", webacc.CreateUser);
            IAddWebAcc addWebAcc = new AddData();
            addWebAcc.AddAccInfo(webacc);
            return View(webacc);
        }
    }
}