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
                ModelState.AddModelError("CredentialError", "账号和密码错误");
                //return RedirectToAction("LoginIndex");
            }
            else if (checkacc.Isadminacc(loginacc)&& checkacc.Isaccountuser(loginacc))
            {
                FormsAuthentication.SetAuthCookie(loginacc.UserName, false);
                return RedirectToAction("Index", "AdminUser");
            }
            else if (!checkacc.Isadminacc(loginacc)&& checkacc.Isaccountuser(loginacc))
            {
                FormsAuthentication.SetAuthCookie(loginacc.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            return View();   
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginIndex");
        }
    }
}