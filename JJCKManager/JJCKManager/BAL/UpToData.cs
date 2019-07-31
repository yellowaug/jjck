using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JJCKManager.Models;
using JJCKManager.DAL;

namespace JJCKManager.BAL
{
    
    public interface IupVmAccount
    {
        VMHostAccount UptableVMAcc(int? id);
    }
    public interface IupWebAccount
    {
        WebManagerAccount UptableWebAcc(int? id);
    }
    public interface IupOthAccount
    {
        OtherAccount UptableOthAcc(int? id);
    }
    public interface IupAccount
    {
        Account UptableSysAcc(int? id);
    }
    
    public class UpToTableData : IupAccount, IupOthAccount, IupVmAccount, IupWebAccount
    {
        private JJCKManagerContext jjckdb = new JJCKManagerContext();

        OtherAccount IupOthAccount.UptableOthAcc(int? id)
        {
            var othres = jjckdb.OtherAccounts.Find(id);
            return othres;

        }
        Account IupAccount.UptableSysAcc(int? id)
        {
            var sysaccres = jjckdb.Accounts.Find(id);
            return sysaccres;
        }
        VMHostAccount IupVmAccount.UptableVMAcc(int? id)
        {
            var vmaccres = jjckdb.HostAccounts.Find(id);
            return vmaccres;
        }
        WebManagerAccount IupWebAccount.UptableWebAcc(int? id)
        {
            var webaccres = jjckdb.ManagerAccounts.Find(id);
            return webaccres;
        }
    }
}