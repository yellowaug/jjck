using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.Models;
using JJCKManager.BAL;
using System.Data;
using JJCKManager.DAL;

namespace JJCKManager.Controllers
{
    [Authorize]
    public class SystemAccountController : Controller
    {
        // GET: SystemAccount
        public ActionResult Index()
        {
            ViewBag.loginname = User.Identity.Name;//显示登录账号
            IAccList accList = new GetAccountList();
            return View(accList.GetAllAccount());
            
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
        [HttpPut]
        public ActionResult AccDetalis(int? id)
        {
            using(var db=new JJCKManagerContext())
            {
                var result = db.Accounts.Find(id);
               
                return View(result);
            }
            
        }

    }
}