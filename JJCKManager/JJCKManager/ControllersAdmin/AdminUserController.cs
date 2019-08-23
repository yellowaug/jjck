using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JJCKManager.BAL;

namespace JJCKManager.ControllersAdmin
{
    public class AdminUserController : Controller
    {
        /// <summary>
        /// 导航页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
       
        ///<summary>
        ///这个是获取全部本系统用户的账号
        ///</summary>
        public ActionResult GetAllAcc()
        {
            IAccList GetAllAccount = new GetAccountList();
            return View(GetAllAccount.GetAllAccount());
        }
        /// <summary>
        /// 这个是获取全部web页面管理的账号
        /// </summary>
       
        public ActionResult GetALLWebManagetAcc()
        {
            IwebAccList GetAllWebAcc = new GetAccountList();
            return View(GetAllWebAcc.GetAllWebAcc());
        }
        /// <summary>
        /// 这个是获取全部虚拟机账号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetALLVMHostAcc()
        {
            IVmAccList GetAllVmAcc = new GetAccountList();
            return View(GetAllVmAcc.GetAllVmAcc());
        }
        /// <summary>
        /// 这个是获取全部杂项账号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetALLOtherAcc()
        {
            IOthAccList GetALLOtherAcc = new GetAccountList();
            return View(GetALLOtherAcc.GetAllOthAcc());
        }

    }
}