using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.IO;

namespace JJCKsqlback
{

    public interface IAutoBackUp
    {
        void AutoBack(Dbinfo dbinfos, OdbcConnection connection);
        void AutoRevert(Dbinfo dbinfos, OdbcConnection connection);
    }
    public interface IGetDbList
    {
        List<Dbinfo> GetDb(OdbcConnection connection, string backPath, string[] dbname);
        List<Dbinfo> GetDbrevert(OdbcConnection connection, string backPath,string[] dbname);
    }
    public interface IConnectionDb
    {
        OdbcConnection InitConnection();
    }
    public interface ICloseConnection
    {
        void Closeconnection(OdbcConnection connection);
    }
    /// <summary>
    /// 自动备份的方法，DBinfo打算是传入LIST的
    /// </summary>
    class DataBase : IAutoBackUp,IGetDbList,IConnectionDb,ICloseConnection
    {
        
        void IAutoBackUp.AutoBack(Dbinfo dbinfos,OdbcConnection connection)
        {           
            OdbcCommand command = new OdbcCommand(dbinfos.SQLShell,connection);
            command.CommandTimeout = 0; //不限制等待时间
            try
            {
                Console.WriteLine($"数据库{dbinfos.DbName}备份中.....");
                if (command.ExecuteNonQuery()==-1)
                {
                    Console.WriteLine($"数据库{dbinfos.DbName}备份成功.....");
                }                              
            }
            catch (InvalidOperationException nonquery)
            {
                
                Console.WriteLine(nonquery.Message);
            }
            catch (NotSupportedException conex)
            {
                Console.WriteLine(conex.Message);
            }
            finally
            {
                command.Dispose();
            }
        }
        /// <summary>
        /// 生成备份语句的方法，还没验证，明天去验证一下
        /// </summary>
        /// <param name="backPath"></param>
        /// <param name="dbname"></param>
        List<Dbinfo> IGetDbList.GetDb(OdbcConnection connection, string backPath, string[] dbname)
        {
            List<Dbinfo> dbinfos = new List<Dbinfo>();
            
            foreach (var db in dbname)
            {
                Dbinfo setDbinfo = new Dbinfo();
                connection.ChangeDatabase(db);
                string backfilePath = Path.Combine(backPath, db);
                string sqlshell= @"BACKUP DATABASE [" + db + "] TO  DISK = N'" + backfilePath + "' WITH NOFORMAT, NOINIT,  NAME = N'" + db + "-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                setDbinfo.PathAndFileName = backfilePath;
                setDbinfo.SQLShell = sqlshell;
                setDbinfo.DbName = connection.Database;               
                dbinfos.Add(setDbinfo);
                
            }
            return dbinfos;
        }

        OdbcConnection IConnectionDb.InitConnection()
        {
            string constr = "Driver={SQL Server};Server=(local);Trusted_Connection=Yes";
            OdbcConnection connection = new OdbcConnection(constr);
            connection.Open();
            return connection;
        }
        /// <summary>
        /// 关闭数据库连接对象
        /// </summary>
        /// <param name="connection"></param>
        void ICloseConnection.Closeconnection(OdbcConnection connection)
        {
            connection.Close();
            connection.Dispose();
        }
        /// <summary>
        /// 生成数据库还原语句
        /// </summary>
        /// <param name="connection">odbc连接对象</param>
        /// <param name="backPath">备份文件的文件路径</param>
        /// <param name="dbname">要备份的数据库名称</param>
        /// <returns>返回DBinfo的列表对象</returns>
        List<Dbinfo> IGetDbList.GetDbrevert(OdbcConnection connection, string backPath,string[] dbname)
        {
            List<Dbinfo> dbinfos = new List<Dbinfo>();

            foreach (var db in dbname)
            {
                Dbinfo setDbrevertinfo = new Dbinfo();
                StringBuilder revertShell = new StringBuilder();
                connection.ChangeDatabase("master");
                string backfilePath = Path.Combine(backPath, db);
                //字符拼接的新实现,拼接时要注意语句中的空格。
                revertShell.AppendFormat(@"RESTORE DATABASE [{0}] FROM  ", db);
                revertShell.AppendFormat(@"DISK = N'{0}' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 5",backfilePath);
                //string sqlshell = @"RESTORE DATABASE ["+db+"] FROM  DISK = N'"+backfilePath+"' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 5";
                setDbrevertinfo.PathAndFileName = backfilePath;
                setDbrevertinfo.SQLShell = revertShell.ToString();
                setDbrevertinfo.DbName = db;
                dbinfos.Add(setDbrevertinfo);
            }
            return dbinfos;
        }
        /// <summary>
        /// 数据库自动还原动作
        /// </summary>
        /// <param name="dbinfos">包含数据库语句信息的属性对象类</param>
        /// <param name="connection">ODBC的连接对象</param>
        void IAutoBackUp.AutoRevert(Dbinfo dbinfos, OdbcConnection connection)
        {
            OdbcCommand command = new OdbcCommand(dbinfos.SQLShell, connection);
            command.CommandTimeout = 0; //不限制等待时间
            try
            {
                Console.WriteLine($"数据库{dbinfos.DbName}还原中.....");
                
                if (command.ExecuteNonQuery() == -1)
                {
                    Console.WriteLine($"数据库{dbinfos.DbName}还原成功.....");
                }
            }
            catch (InvalidOperationException nonquery)
            {

                Console.WriteLine(nonquery.Message);
            }
            catch (NotSupportedException conex)
            {
                Console.WriteLine(conex.Message);
            }
            finally
            {
                command.Dispose();
            }
        }
    }
}
