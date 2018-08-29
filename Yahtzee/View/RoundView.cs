using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    enum DisplayType { ViewVerboseList, ViewSavedGame, ResumeSavedGame, ViewAvaialbleCombos }
    class RoundView : Display
    {
        private readonly string viewVerboseList = "\nView verbose scoreboard (y) or compact scoreboard (n) of this game (y/n)";
        private readonly string viewSavedGame = "\nView saved games? (y/n)";
        private readonly string resumeSavedGame = "\nResume saved game? (y/n)";
        private readonly string viewAvailableCombos = "\nView available combinations? (y/n)";
        public void RenderRoundNumber(int roundNumber)
        {
            PrintMessage("\nRound number " + roundNumber);
        }
        public void RenderRound(string name)
        {
            PrintMessage("\n" + name + ", it's your turn!");
        }
        public void RenderDice(int[] dice)
        {
            string output = "";
            Console.WriteLine("");
            for (int i = 1; i <= dice.Length; i++)
            {
                output += "Dice number: " + i + " Value: " + dice[i - 1] + "\n";
            }
            Console.Write(output);
        }
        public void RenderDiceToRoll(bool[] diceToRoll, string decision = "")
        {
            bool stand = true;
            for (int i = 0; i < diceToRoll.Length; i++)
            {
                if (diceToRoll[i])
                    stand = false;
            }
            if (stand)
                Console.Write("\n" + decision + "\n");
            else
            {
                Console.Write("\n" + decision + "Decided to reroll: ");
                for (int i = 0; i < diceToRoll.Length; i++)
                {
                    if (diceToRoll[i])
                    {
                        Console.Write(i + 1 + " ");
                    }
                }
                Console.WriteLine("");
            }
        }
        public bool[] GetDiceToRoll()
        {
            bool[] diceToRoll = { };
            bool getInput = true;
            int val;
            int index;
            while (getInput)
            {
                diceToRoll = new bool[] { false, false, false, false, false };
                Console.WriteLine("Select dice to roll. Enter the numbers of your dices separated by a space, or 0 to stand");
                string input = Console.ReadLine();
                string[] diceNumbers = input.Split(' ');
                getInput = false;
                if (Int32.TryParse(diceNumbers[0], out val) && val == 0)
                {
                    return diceToRoll;
                }
                for (int i = 0; i < diceNumbers.Length; i++)
                {
                    if (Int32.TryParse(diceNumbers[i], out index) && index >= 1 && index <= 5)
                    {
                        diceToRoll[index - 1] = true;
                    }
                    else
                    {
                        PrintErrorMessage("Invalid input");
                        getInput = true;
                        break;
                    }
                }
            }
            return diceToRoll;
        }
        public void RenderUnavailableCombos(List<Combo> unavailableCombos)
        {
            RenderComboList(unavailableCombos);
        }
        public Combo RenderCombo(List<Combo> unavailableCombos)
        {
            int enumLength = ComboModel.GetSize();
            while (true)
            {
                int value = 0;
                PrintMessage("\nSelect combination from the list: ");
                RenderComboList(unavailableCombos);

                if (Int32.TryParse(Console.ReadLine(), out value) && value >= 1 && value < enumLength + 1)
                {
                    bool exist = unavailableCombos.Contains((Combo)(value - 1));
                    if (!exist)
                        return ComboModel.GetCombo(value - 1);
                }
                PrintErrorMessage("Invalid input, enter available number.");
            }
        }
        private void RenderComboList(List<Combo> unavailableCombos)
        {
            string output = "";
            foreach (Combo c in ComboModel.GetList())
            {
                bool exist = unavailableCombos.Contains(c);

                if (exist)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                output = "(" + ((int)c + 1) + ") " + c;

                Console.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public bool ContinueGame()
        {
            do
            {
                PrintMessage("\nContinue game (y/n)");

                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    Console.Clear();
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    Console.Clear();
                    return false;
                }
                PrintErrorMessage("Invalid input, answer with (y/n).");
            } while (true);
        }
        public bool SelectActivity(DisplayType displayType, bool clear = true)
        {
            string message = "";
            switch (displayType)
            {
                case DisplayType.ViewVerboseList:
                    message = viewVerboseList;
                    break;
                case DisplayType.ViewSavedGame:
                    message = viewSavedGame;
                    break;
                case DisplayType.ResumeSavedGame:
                    message = resumeSavedGame;
                    break;
                case DisplayType.ViewAvaialbleCombos:
                    message = viewAvailableCombos;
                    break;
                default:
                    break;
            }
            do
            {
                PrintMessage(message);

                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    if (clear)
                        Console.Clear();
                    return false;
                }
                PrintErrorMessage("Invalid input, answer with (y/n).");
            } while (true);
        }
        public void SaveGame(string fileName)
        {
            PrintMessage("Game saved: " + fileName);
        }
        public void FinishedGame(string name, int score)
        {
            PrintMessage("*************************************************");
            PrintMessage(" The winner is " + name + " with score " + score);
            PrintMessage("*************************************************");
        }
        public string GetSavedGame(FileInfo[] files)
        {
            Console.Clear();
            string selectedFile = "";

            PrintMessage("\nSelect file from the list.");
            PrintMessage("Press any other key if you wish to return.");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine("(" + i + ") " + files[i].Name);
            }
            string input = Console.ReadLine();
            int index = 0;
            if (Int32.TryParse(input, out index) && (index >= 0) && (index < files.Length))
            {
                Console.Clear();
                PrintMessage("\nGame " + files[index].Name + " selected");
                selectedFile = files[index].Name;
            }
            return selectedFile;
        }
    }
}