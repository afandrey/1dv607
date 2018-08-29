using System;
using BoatClub.model;

namespace BoatClub.view
{
    class BoatView
    {
        BoatModel bm = new BoatModel();
        private enum MenuOptions
        {
            ExitBoatMenu = 0,
            AddBoat = 1,
            EditBoat = 2,
            RemoveBoat = 3,
        }
        public void AddBoat(BoatModel boat)
        {
            int memberId;
            string type;
            int length;

            Console.Clear();
            Console.WriteLine("Add new boat");
            Console.WriteLine("Enter member id");
            memberId = Int32.Parse(Console.ReadLine());

            Console.WriteLine("1: Sailboat | 2: Motorboat | 3: Kayak/Canoe | 4: Others");
            type = Console.ReadLine();

            if (type == "1")
            {
                type = "Sailboat";
            }
            else if (type == "2")
            {
                type = "Motorboat";
            }
            else if (type == "3")
            {
                type = "Kayak/Canoe";
            }
            else if (type == "4")
            {
                type = "Other";
            }
            else
            {
                Console.WriteLine("Not a valid type");
            }

            Console.WriteLine("Enter length");
            length = Int32.Parse(Console.ReadLine());

            boat.AddBoat(memberId, type, length);
            Console.WriteLine("Boat added");
        }
        public void EditBoat(BoatModel boat)
        {
            string type;
            int memberId;
            int length;
            int boatId;

            Console.Clear();
            Console.WriteLine("Edit Boat");
            Console.WriteLine("Enter member id");
            memberId = Int32.Parse(Console.ReadLine());

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter boatId");
                    boatId = Int32.Parse(Console.ReadLine());
                    try
                    {
                        boat.GetBoat(memberId, boatId);
                    }
                    catch
                    {
                        Console.WriteLine("No matching boatId");
                        SafeExit();
                        return;
                    }
                    Console.WriteLine("Enter new type");
                    type = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("Enter new length");
                        length = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        length = 0;
                    }
                    boat.EditBoat(memberId, boatId, type, length);
                    Console.WriteLine("Boat edited");
                }
                catch (Exception)
                {
                    Console.WriteLine("No matching boatId");
                }
                SafeExit();
                break;
            }
        }
        public void RemoveBoat(BoatModel boat)
        {
            int memberId;
            int boatId;

            Console.Clear();
            Console.WriteLine("Remove Boat");
            Console.WriteLine("Enter member id");
            memberId = Int32.Parse(Console.ReadLine());

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter boatId");
                    boatId = Int32.Parse(Console.ReadLine());
                    try
                    {
                        boat.RemoveBoat(memberId, boatId);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("No matching boatId");
                        SafeExit();
                        return;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("No matching boatId");
                    SafeExit();
                    return;
                }
            }
            Console.WriteLine("Boat removed");
            SafeExit();
        }
        public void BoatMenu()
        {
            while (true)
            {
                int menuOptions = MainBoat();

                switch (menuOptions)
                {
                    case (int)MenuOptions.ExitBoatMenu:
                        return;
                    case (int)MenuOptions.AddBoat:
                        AddBoat(bm);
                        return;
                    case (int)MenuOptions.EditBoat:
                        EditBoat(bm);
                        return;
                    case (int)MenuOptions.RemoveBoat:
                        RemoveBoat(bm);
                        return;
                    default:
                        break;
                }
            }
        }
        public int MainBoat()
        {
            int index;

            do
            {
                Console.Clear();
                Console.WriteLine("0: Exit");
                Console.WriteLine("1: Add Boat");
                Console.WriteLine("2: Edit Boat");
                Console.WriteLine("3: Remove Boat");

                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 3)
                {
                    return index;
                }
                else
                {
                    Console.WriteLine("Enter a number 0-3");
                    SafeExit();
                }
            }
            while (true);
        }
        private void SafeExit()
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}