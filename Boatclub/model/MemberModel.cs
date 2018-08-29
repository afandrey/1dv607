using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace BoatClub.model
{
    class MemberModel
    {
        private string name;
        private int _date;
        private int _Id;
        public List<BoatModel> Boats = new List<BoatModel>();
        Database db = new Database();
        public string FullName
        {
            get { return name; }
            set { name = value; }
        }
        public int Birthday
        {
            get { return _date; }
            set { _date = value; }
        }
        public int MemberId
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public void AddMember(string name, int bday)
        {
            var xmlDoc = db.GetDocument();

            int memberId = ((from member in xmlDoc.Descendants("Member")
                          select (int)member.Attribute("memberId")).DefaultIfEmpty(0).Max()) + 1;

            xmlDoc.Descendants("Members")
                    .FirstOrDefault()
                    .Add(new XElement("Member",
                    new XAttribute("memberId", memberId),
                    new XAttribute("name", name),
                    new XAttribute("birthday", bday),
                    new XElement("Boats")));

            xmlDoc.Save(db.Path);
        }

        public void EditMember(int memberId, string name = null, int bday = 0)
        {
            var xmlDoc = db.GetDocument();

            if (name.Length > 1)
            {
                xmlDoc.Descendants("Member")
                        .Where(x => (int)x.Attribute("memberId") == memberId)
                        .Single()
                        .SetAttributeValue("name", name);
            }
            if (bday != 0)
            {
                xmlDoc.Descendants("Member")
                        .Where(x => (int)x.Attribute("memberId") == memberId)
                        .Single()
                        .SetAttributeValue("birthday", bday);
            }
            xmlDoc.Save(db.Path);
        }

        public void RemoveMember(int memberId)
        {
            var xmlDoc = db.GetDocument();

            if (GetMember(memberId) != null)
            {
                xmlDoc.Descendants("Member")
                        .Where(x => (int)x.Attribute("memberId") == memberId)
                        .Remove();
                xmlDoc.Save(db.Path);
            }
        }
        public MemberModel GetMember(int memberId)
        {
            var xmlDoc = db.GetDocument();

            var singleMember = (from member in xmlDoc.Descendants("Member").Where(x => (int)x.Attribute("memberId") == memberId)
                                select new MemberModel
                                {
                                    FullName = (string)member.Attribute("name"),
                                    Birthday = (int)member.Attribute("birthday"),
                                    MemberId = (int)member.Attribute("memberId"),
                                    Boats = (from boat in member.Descendants("Boat")
                                             select new BoatModel
                                             {
                                                 BoatId = (int)boat.Attribute("boatId"),
                                                 BoatType = (string)boat.Attribute("type"),
                                                 Length = (int)boat.Attribute("length")
                                             }).ToList()
                                }).Single();

            return singleMember;
        }
        public IEnumerable<MemberModel> ShowAllMembers()
        {
            var xmlDoc = db.GetDocument();

            var members = (from member in xmlDoc.Descendants("Member")
                           select new MemberModel
                           {
                               FullName = (string)member.Attribute("name"),
                               Birthday = (int)member.Attribute("birthday"),
                               MemberId = (int)member.Attribute("memberId"),
                               Boats = (from boat in member.Descendants("Boat")
                                        select new BoatModel
                                        {
                                            BoatId = (int)boat.Attribute("boatId"),
                                            BoatType = (string)boat.Attribute("type"),
                                            Length = (int)boat.Attribute("length")
                                        }).ToList()
                           }).ToList();

            return members;
        }
    }
}