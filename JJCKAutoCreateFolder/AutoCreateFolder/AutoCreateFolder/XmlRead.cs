using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutoCreateFolder
{
    public class XmlRead
    {
        public string ReadXmlPath()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"PathConfig.xml");
            XmlNode xnroot = doc.SelectSingleNode("path");
            XmlNodeList xmlist = xnroot.ChildNodes;
            return xmlist.Item(0).InnerText;
            
        }
        
    }
}
