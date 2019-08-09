using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.IO;

namespace JJCKsqlback
{
    public class Dbinfo
    {
        public string DbName { get; set; }
        public string PathAndFileName { get; set; }
        public string SQLShell { get; set; }
    }
    public interface IAutoBackUp
    {
        void AutoBack(Dbinfo dbinfos, OdbcConnection connection);
    }
    public interface IGetDbList
    {
        void GetDb(string backPath, string[] dbname);
    }
    /// <summary>
    /// 自动备份的方法，DBinfo打算是传入LIST的
    /// </summary>
    class DataBase : IAutoBackUp,IGetDbList
    {
        
        void IAutoBackUp.AutoBack(Dbinfo dbinfos,OdbcConnection connection)
        {
            //string constr = "Driver={SQL Server};Server=(local);Trusted_Connection=Yes;Database=EFtest";
            //string sqlShell = @"BACKUP DATABASE [EFtest] TO  
            //                  DISK = N'F:\test\EFtest'WITH NOFORMAT, NOINIT,  
            //                  NAME = N'EFtest-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            
            
            //OdbcConnection connection = new OdbcConnection(constr);
            //connection.Open();
            //string backfilePath = Path.Combine(backPath, connection.Database);
            //string sqlshell = @"BACKUP DATABASE [Book] TO  DISK = N'" + backfilePath + "' WITH NOFORMAT, NOINIT,  NAME = N'EFtest-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            OdbcCommand command = new OdbcCommand(dbinfos.SQLShell,connection);
            try
            {                
                Console.WriteLine(command.ExecuteNonQuery()); 
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
                connection.Close();
                connection.Dispose();
            }
        }
        /// <summary>
        /// 生成备份语句的方法，还没验证，明天去验证一下
        /// </summary>
        /// <param name="backPath"></param>
        /// <param name="dbname"></param>
        void IGetDbList.GetDb(string backPath, string[] dbname)
        {
            List<Dbinfo> dbinfos = new List<Dbinfo>();
            Dbinfo setDbinfo = new Dbinfo();
            string constr = "Driver={SQL Server};Server=(local);Trusted_Connection=Yes;Database=EFtest";
            
            OdbcConnection connection = new OdbcConnection(constr);
            connection.Open();
            string backfilePath = Path.Combine(backPath, connection.Database);
            string sqlshell = @"BACKUP DATABASE ["+ connection.Database + "] TO  DISK = N'" + backfilePath + "' WITH NOFORMAT, NOINIT,  NAME = N'"+ connection.Database + "-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            setDbinfo.PathAndFileName = backfilePath;
            setDbinfo.SQLShell = sqlshell;
            dbinfos.Add(setDbinfo); //到这就是生成一个备份语句的结束
            foreach (var db in dbname)
            {
                connection.ChangeDatabase(db);
                backfilePath = Path.Combine(backPath, db);
                sqlshell= @"BACKUP DATABASE [" + db + "] TO  DISK = N'" + backfilePath + "' WITH NOFORMAT, NOINIT,  NAME = N'" + db + "-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                setDbinfo.PathAndFileName = backfilePath;
                setDbinfo.SQLShell = sqlshell;
                dbinfos.Add(setDbinfo);
            }

        }
    }
}
