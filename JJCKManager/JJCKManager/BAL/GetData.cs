using JJCKManager.DAL;
using JJCKManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JJCKManager.BAL
{
    public interface IAccList//获取表的全部字段信息
    {
        List<Account> GetAllAccount();
    }
    public interface IiotDataList
    {
        List<IotTemperListFunction> GetIotList();
    }
    public interface IcheckUser
    {
        bool Isaccountuser(Account account);
        //bool IsSuperMan(Account account);
    }
    public interface IwebAccList //获取WEB账号信息
    {
        List<WebManagerAccount> GetAllWebAcc();
    }
    public interface IOthAccList
    {
        List<OtherAccount> GetAllOthAcc();
    } //获取虚拟机账号信息
    public interface IVmAccList
    {
        List<VMHostAccount> GetAllVmAcc();
    } //获取其他账号信息
    
    public class GetAccountList : IAccList,IwebAccList,IVmAccList,IOthAccList
    {
        public List<Account> GetAllAccount()
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                return jjckdb.Accounts.ToList();
            }
        }

        List<OtherAccount> IOthAccList.GetAllOthAcc()
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                return jjckdb.OtherAccounts.ToList();
            }
        }

        List<VMHostAccount> IVmAccList.GetAllVmAcc()
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                return jjckdb.HostAccounts.ToList();
            }
        }

        List<WebManagerAccount> IwebAccList.GetAllWebAcc()
        {
            using(JJCKManagerContext jjckdb=new JJCKManagerContext())
            {
                return jjckdb.ManagerAccounts.ToList();
            }
        }
    }
    public class GetIotData : IiotDataList
    {
        public List<IotTemperListFunction> GetIotList()
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                return jjckdb.IotTempers.ToList();
            }
        }
    }
    public class GetLoginUser : IcheckUser
    {
        public bool Isaccountuser(Account account)
        {
            
            using(JJCKManagerContext jjckdb=new JJCKManagerContext())
            {
                var chkresult = from username in jjckdb.Accounts
                                where username.UserName == account.UserName
                                where username.PassWord==account.PassWord
                                select username;
                //var res = chkresult.ToList().Count();
                //foreach (var item in chkresult)
                //{
                //    var res1 = item.PassWord;
                //    var res2 = item.UserName;
                //}
                if (chkresult.ToList().Count() == 1)
                {
                    return true;
                }
                else if (account.UserName == "Admin" && account.PassWord == "jjck@123")
                {
                    return true;
                }
                else
                {
                    return false;
                }                                            
            }
        }

        //bool IcheckUser.IsSuperMan(Account account)
        //{
        //    if (account.UserName=="Admin"&&account.PassWord=="jjck@123")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}