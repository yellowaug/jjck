using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJCKsqlback
{
    public class Dbinfo
    {
        public string DbName { get; set; }
        public string PathAndFileName { get; set; }
        public string SQLShell { get; set; }
    }
    public class Dirinfo
    {
        public string FolderName { get; set; }
        public string RootPath { get; set; }
        public string FullPath { get; set; }

    }
    public class RunTimming
    {
        public int RunDay { get; set; }
        public int RunCount { get; set; }
        public int RunMount { get; set; }
    }
}
