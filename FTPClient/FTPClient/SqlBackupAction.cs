using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;

namespace FTPClient
{
    class Dbinfo
    {
        public string DbName { get; set; }
        public string PathAndFileName { get; set; }
        public string SQLShell { get; set; }
    }
    interface IConnectionDb
    {
        OdbcConnection InitConnection();
        void Closeconnection(OdbcConnection connection);
    }
    interface IRevertDbAction
    {
        List<Dbinfo> GetDbrevert(OdbcConnection connection, string backPath, string[] dbname);
        void AutoRevert(Dbinfo dbinfos, OdbcConnection connection);
    }

    public class SQLBackupAction : IConnectionDb, IRevertDbAction
    {

        void IRevertDbAction.AutoRevert(Dbinfo dbinfos, OdbcConnection connection)
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
        List<Dbinfo> IRevertDbAction.GetDbrevert(OdbcConnection connection, string backPath, string[] dbname)
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
                revertShell.AppendFormat(@"DISK = N'{0}' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 5", backfilePath);
                //string sqlshell = @"RESTORE DATABASE ["+db+"] FROM  DISK = N'"+backfilePath+"' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 5";
                setDbrevertinfo.PathAndFileName = backfilePath;
                setDbrevertinfo.SQLShell = revertShell.ToString();
                setDbrevertinfo.DbName = db;
                dbinfos.Add(setDbrevertinfo);
            }
            return dbinfos;
        }

        void IConnectionDb.Closeconnection(OdbcConnection connection)
        {
            connection.Close();
            connection.Dispose();
        }



        OdbcConnection IConnectionDb.InitConnection()
        {
            string constr = "Driver={SQL Server};Server=(local);Trusted_Connection=Yes";
            OdbcConnection connection = new OdbcConnection(constr);
            connection.Open();
            return connection;
        }
    }
}
