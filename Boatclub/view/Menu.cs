using System;
using BoatClub.model;

namespace BoatClub.view
{
    class Menu
    {
        MemberModel mm = new MemberModel();
        MemberView mv = new MemberView();
        BoatModel bm = new BoatModel();
        BoatView bv = new BoatView();
        private enum MenuOptions
        {
            Exit = 0,
            AddMember = 1,
            EditMember = 2,
            RemoveMember = 3,
            ShowUser = 4,
            VerboseMemberList = 5,
            CompactMemberList = 6,
            ManageBoats = 7,
        }
        public void Start()
        {
            while (true)
            {
                int menuOptions = MainMenu();

                switch (menuOptions)
                {
                    case (int)MenuOptions.Exit:
                        Environment.Exit(0);
                        return;
                    case (int)MenuOptions.AddMember:
                        mv.AddMember(mm);
                        return;
                    case (int)MenuOptions.EditMember:
                        mv.EditMember(mm);
                        return;
                    case (int)MenuOptions.RemoveMember:
                        mv.RemoveMember(mm);
                        return;
                    case (int)MenuOptions.ShowUser:
                        mv.DisplayMember(mm);
                        return;
                    case (int)MenuOptions.VerboseMemberList:
                        mv.VerboseList(mm, bm);
                        return;
                    case (int)MenuOptions.CompactMemberList:
                        mv.CompactList(mm, bm);
                        return;
                    case (int)MenuOptions.ManageBoats:
                        bv.BoatMenu();
                        return;
                    default:
                        break;
                }
            }
        }
        private int MainMenu()
        {
            int index;
            do
            {
                //Console.Clear();
                Console.WriteLine("0: Exit");
                Console.WriteLine("1: Add Member");
                Console.WriteLine("2: Edit Member");
                Console.WriteLine("3: Remove Member");
                Console.WriteLine("4: Show Single User");
                Console.WriteLine("5: View Verbose List");
                Console.WriteLine("6: View Compact List");
                Console.WriteLine("7: Manage Boats");

                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 7)
                {
                    return index;
                }
                else
                {
                    Console.WriteLine("Enter a number 0-7");
                    SafeExit();
                }
            } while (true);
        }
        private void SafeExit()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}