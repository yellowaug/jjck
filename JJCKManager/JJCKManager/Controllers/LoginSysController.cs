using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.Models;
using JJCKManager.BAL;
using System.Web.Security;

namespace JJCKManager.Controllers
{
    public class LoginSysController : Controller
    {
        // GET: LoginSys
        public ActionResult LoginIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginIndex(Account loginacc)//这个的实现还有一些问题
        {
            IcheckUser checkacc = new GetLoginUser();
            //var userName=checkacc.accountuser(loginacc);
            if (!checkacc.Isaccountuser(loginacc))
            {
                
                    
                ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                return RedirectToAction("LoginIndex");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(loginacc.UserName, false);
                
                return RedirectToAction("Index", "Home");
            }
               
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("LoginIndex");
        }
    }
}