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
        void AutoBack(string backPath);
    }
    class DataBase : IAutoBackUp
    {
        void IAutoBackUp.AutoBack(string backPath)
        {
            string constr = "Driver={SQL Server};Server=(local);Trusted_Connection=Yes;Database=EFtest;";
            //string sqlShell = @"BACKUP DATABASE [EFtest] TO  
            //                  DISK = N'F:\test\EFtest'WITH NOFORMAT, NOINIT,  
            //                  NAME = N'EFtest-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            
            
            OdbcConnection connection = new OdbcConnection(constr);
            connection.Open();
            string backfilePath = Path.Combine(backPath, connection.Database);
            string sqlshell = @"BACKUP DATABASE [Book] TO  DISK = N'" + backfilePath + "' WITH NOFORMAT, NOINIT,  NAME = N'EFtest-完整 数据库 备份', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            OdbcCommand command = new OdbcCommand(sqlshell,connection);
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
                
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
