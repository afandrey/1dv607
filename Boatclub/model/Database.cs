using System;
using System.Xml;
using System.Xml.Linq;

namespace BoatClub
{
    class Database
    {
        private string _filePath = "BoatClub.xml";
        XDocument xmlDoc;
        public string Path
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
        public XDocument GetDocument()
        {
            return xmlDoc;
        }
        public Database()
        {
            xmlDoc = XDocument.Load(Path);
        }
    }
}