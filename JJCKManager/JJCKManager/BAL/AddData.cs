using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using JJCKManager.DAL;
using JJCKManager.Models;

namespace JJCKManager.BAL
{
    //public interface IAccList//获取表的全部字段信息
    //{
    //    List<Account> GetAllAccount();
    //}
    public interface IAddData
    {
        void addAccount(Account account);
    }//添加数据
    public interface IAddIotData
    {
        void addIotdata(IotTemperListFunction iotTemper);
    }
    public interface IAddWebAcc
    {
        void AddAccInfo(WebManagerAccount webacc);
    }
    public interface IAddVmAcc
    {
        void AddVmAcc(VMHostAccount vmhostacc);
    }
    public interface IAddOtrherAcc
    {
        void AddOtAcc(OtherAccount otacc);
    }
    //public interface IiotDataList
    //{
    //    List<IotTemperListFunction> GetIotList();
    //}

    public class AddData : IAddData,IAddIotData,IAddWebAcc,IAddVmAcc,IAddOtrherAcc
    {
        /// <summary>
        /// 添加系统用户的方法
        /// </summary>
        /// <param name="account"></param>
        public void addAccount( Account account)
        {
            using(JJCKManagerContext jjckdb=new JJCKManagerContext())
            {
                jjckdb.Accounts.Add(account);
                jjckdb.SaveChanges();
            }
            
        }
        /// <summary>
        /// 添加Web管理账号
        /// </summary>
        /// <param name="webacc"></param>
        void IAddWebAcc.AddAccInfo(WebManagerAccount webacc)
        {
            using(JJCKManagerContext jjckdb=new JJCKManagerContext())
            {                
                jjckdb.ManagerAccounts.Add(webacc);
                jjckdb.SaveChanges();
            }
        }

        void IAddIotData.addIotdata(IotTemperListFunction iotTemper)
        {
            using (JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                jjckdb.IotTempers.Add(iotTemper);
                jjckdb.SaveChanges();
            }
        }
        /// <summary>
        /// 添加其他账号
        /// </summary>
        /// <param name="otacc"></param>
        void IAddOtrherAcc.AddOtAcc(OtherAccount otacc)
        {
            using(JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                jjckdb.OtherAccounts.Add(otacc);
                jjckdb.SaveChanges();
            }
        }
        /// <summary>
        /// 添加虚拟虚拟机的登录账号
        /// </summary>
        /// <param name="vmhostacc"></param>
        void IAddVmAcc.AddVmAcc(VMHostAccount vmhostacc)
        {
           using(JJCKManagerContext jjckdb = new JJCKManagerContext())
            {
                jjckdb.HostAccounts.Add(vmhostacc);
                jjckdb.SaveChanges();
            }
        }

    }

}