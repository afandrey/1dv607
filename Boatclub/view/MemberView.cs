using System;
using System.Collections.Generic;
using BoatClub.model;

namespace BoatClub.view
{
    class MemberView
    {
        public void AddMember(MemberModel member)
        {
            string name;
            int bday;

            Console.Clear();
            Console.WriteLine("Enter name");
            name = Console.ReadLine();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter birthday, 10 numbers");
                    bday = Int32.Parse(Console.ReadLine());

                    if (bday.ToString().Length != 10)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Birthday not correctly entered");
                }
            }
            try
            {
                member.AddMember(name, bday);
                Console.WriteLine("Member saved");
            }
            catch (Exception)
            {
                Console.WriteLine("Member could not be saved");
            }
            SafeExit();
        }
        public void EditMember(MemberModel member)
        {
            string name;
            int bday;
            int memberId;

            Console.Clear();
            Console.WriteLine("Edit Member");

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter memberId");
                    memberId = Int32.Parse(Console.ReadLine());
                    try
                    {
                        member.GetMember(memberId);
                    }
                    catch
                    {
                        Console.WriteLine("No matching memberId");
                        SafeExit();
                        return;
                    }
                    Console.WriteLine("Enter a name");
                    name = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("Enter new birthday, 10 numbers");
                        bday = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        bday = 0;
                    }
                    member.EditMember(memberId, name, bday);
                    Console.WriteLine("Member edited");
                }
                catch (Exception)
                {
                    Console.WriteLine("No matching memberId");
                }
                SafeExit();
                break;
            }
        }
        public void RemoveMember(MemberModel member)
        {
            int memberId;

            Console.Clear();
            Console.WriteLine("Remove Member");

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter memberId");
                    memberId = Int32.Parse(Console.ReadLine());
                    try
                    {
                        member.RemoveMember(memberId);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No matching memberId");
                        SafeExit();
                        return;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("No matching memberId");
                    SafeExit();
                    return;
                }
            }
            Console.WriteLine("Member removed");
            SafeExit();
        }
        public void DisplayMember(MemberModel member)
        {
            int memberId;

            Console.Clear();
            Console.WriteLine("Enter memberId");
            memberId = Int32.Parse(Console.ReadLine());

            IEnumerable<MemberModel> members = member.ShowAllMembers();

            foreach (var m in members)
            {
                if (memberId == m.MemberId)
                {
                    Console.WriteLine("ID: {0}, Name: {1}, Birthday: {2}", m.MemberId, m.FullName, m.Birthday);
                    
                    foreach (var b in m.Boats)
                    {
                        Console.WriteLine("Boat ID: {0}", b.BoatId);
                        Console.WriteLine("Boat Type: {0}", b.BoatType);
                        Console.WriteLine("Boat Length: {0} ft", b.Length);
                        Console.WriteLine("---------------------");
                    }
                }
            }

            SafeExit();
        }
        public void CompactList(MemberModel member, BoatModel boat)
        {
            Console.Clear();

            IEnumerable<MemberModel> members = member.ShowAllMembers();

            foreach (var m in members)
            {
                Console.WriteLine("ID: {0}, Name: {1}, Number of Boats: {2}", m.MemberId, m.FullName, m.Boats.Count);
                Console.WriteLine("---------------------");
            }
            SafeExit();
        }
        public void VerboseList(MemberModel member, BoatModel boat)
        {
            Console.Clear();
            
            IEnumerable<MemberModel> members = member.ShowAllMembers();

            foreach (var m in members)
            {
                Console.WriteLine("ID: {0}, Name: {1}, Birthday: {2}", m.MemberId, m.FullName, m.Birthday);

                foreach (var b in m.Boats)
                {
                    Console.WriteLine("Boat ID: {0}", b.BoatId);
                    Console.WriteLine("Boat Type: {0}", b.BoatType);
                    Console.WriteLine("Boat Length: {0} ft", b.Length);
                    Console.WriteLine("---------------------");
                }

                Console.WriteLine("---------------------");
            }
            SafeExit();
        }
        private void SafeExit()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}