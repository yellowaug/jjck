using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutoMoveFile
{
    public class ReadXml
    {
        public List<string> SetConfig()
        {
            List<string> pathList = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../PathConfig.xml");
            XmlNode xnroot = doc.SelectSingleNode("path");
            XmlNodeList xmlist = xnroot.ChildNodes;
            for (int i = 0; i < xmlist.Count; i++)
            {
               pathList.Add(xmlist.Item(i).InnerText);
            }
            return pathList;
        }
    }
}
