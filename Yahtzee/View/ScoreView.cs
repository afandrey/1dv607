using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class ScoreView : Display
    {
        public void GetRoundScore(int roundScore, Combo usedCombo)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            PrintMessage("Received " + roundScore + " points for " + ComboModel.GetName(usedCombo) + "\n");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void RenderScoreBoard(List<Player> players, string date, bool verboseList)
        {
            if (date != null)
                Console.WriteLine("\nGame played " + date);

            string divider = "|";
            string end = "";
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < players.Count() + 2; i++)
            {
                divider += "--------";
                end += "********";
            }
            divider += "|";
            Console.WriteLine("\nSCOREBOARD");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("|" + end + "|");
            Console.Write("|\t\t");
            foreach (Player p in players)
            {
                Console.Write(p.Name + "\t");
            }
            Console.WriteLine(" |\n" + divider);
            if (verboseList)
            {
                foreach (Combo c in ComboModel.GetList())
                {
                    Console.Write("|" + c + "\t");
                    if (c <= Combo.Sixes || c == Combo.Chance)
                        Console.Write("\t");
                    foreach (Player p in players)
                    {
                        bool exist;
                        Console.Write(p.GetScore(c, out exist) + "\t");
                    }
                    Console.WriteLine(" |\n" + divider);
                }
            }
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Sum\t\t");
            foreach (Player p in players)
            {
                Console.Write(p.GetTotalScore() + "\t");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" |\n|" + end + "|");
            Console.ResetColor();
        }
    }
}