using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class SetupView : Display
    {
        private readonly string mainMenu = "Welcome to a game of Yahtzee! \n\n";
        public int NumberOfPlayers()
        {
            int value = 0;
            Console.WriteLine("How many players (1-5): ");
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out value) && value <= 5 && value >= 1)
                {
                    return value;
                }
                Console.WriteLine("Invalid input value, value needs to be between 1 and 5.");
            }
        }
        public string GetPlayerName(int number)
        {
            do{
                Console.WriteLine("Player number " + number + ": Enter your name (3-10 characters): ");
                string input = Console.ReadLine().ToLower();
                if (input.Length <= 8 && input.Length >= 3)
                {
                    return input;
                }
                Console.WriteLine("Invalid input.");
            } while (true);
        }
        public bool IsAI(int number)
        {
            do
            {
                Console.WriteLine("Player number " + number + ": Is this player a robot (y/n)");
                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    Console.WriteLine("AI player created successfully");
                    Thread.Sleep(1000);
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    return false;
                }
                Console.WriteLine("Invalid input, answer with (y/n).");
            } while (true);
           
        }
        public SetupView()
        {
            Console.WriteLine(mainMenu);
        }
    }
}