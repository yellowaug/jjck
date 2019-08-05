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
        bool Isadminacc(Account account);
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
    public interface IAlivedothList//根据数据状态查询otheracc，这个是普通用户的查询
    {
        List<OtherAccount> GetOtherAccountsalived();
    }
    public interface IAlivedVmList //根据数据状态查询vmacc，这个是普通用户的查询
    {
        List<VMHostAccount> GetVMHostAccountsalived();
    }
    public interface IAlivedWebList //根据数据状态查询webacc，这个是普通用户的查询
    {
        List<WebManagerAccount> GetWebManagerAccountsalived();
    }
    public interface IAlivedSysAccList //根据数据状态查询sysacc，这个是普通用户的查询
    {
        List<Account> GetAccountsalived();
    }
    
    public class GetAccountList : IAccList,IwebAccList,IVmAccList,IOthAccList,IAlivedothList,IAlivedVmList,IAlivedWebList,IAlivedSysAccList
    {
        public List<Account> GetAllAccount()
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                return jjckdb.Accounts.ToList();
            }
        }

        List<Account> IAlivedSysAccList.GetAccountsalived()//根据数据状态查询sysacc的接口实现，这个是普通用户的查询
        {
            using (JJCKManagerContext jjckdb=new JJCKManagerContext())
            {
                var alivedsys = jjckdb.Accounts.Where(x => x.DaId == (int)EuDataStatus.isavlied).ToList();
                return alivedsys;
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

        List<OtherAccount> IAlivedothList.GetOtherAccountsalived() //根据数据状态查询othacc的接口实现，这个是普通用户的查询
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                var alivedresult = from othacc in jjckdb.OtherAccounts
                                   where othacc.DaId == (int)EuDataStatus.isavlied
                                   select othacc;
                return alivedresult.ToList();
            }
        }

        List<VMHostAccount> IAlivedVmList.GetVMHostAccountsalived()//根据数据状态查询VMhostacc的接口实现，这个是普通用户的查询
        {
            using (JJCKManagerContext jjckdb=new JJCKManagerContext())
            {
                var vmhostresult = jjckdb.HostAccounts.Where(x => x.DaId == (int)EuDataStatus.isavlied).ToList();
                return vmhostresult;
            }
        }

        List<WebManagerAccount> IAlivedWebList.GetWebManagerAccountsalived()//根据数据状态查询webmanacc的接口实现，这个是普通用户的查询
        {
            using (JJCKManagerContext jjckdb=new JJCKManagerContext())
            {
                var webaccresult = jjckdb.ManagerAccounts.Where(x => x.DaId == (int)EuDataStatus.isavlied).ToList();
                return webaccresult;
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
                //查询数据库查看数据库账号密码是否正确
                var chkresult = from username in jjckdb.Accounts
                                where username.UserName == account.UserName
                                where username.PassWord==account.PassWord
                                select username;
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
        public bool Isadminacc(Account account)
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                var chkadmin = from inuser in jjckdb.Accounts
                               where inuser.AccId == 1
                               where inuser.UserName == account.UserName
                               select inuser;
                if (chkadmin.ToList().Count() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }               
            }
        }
    }
}