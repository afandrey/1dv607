using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Database
    {
        private static string path = $"{Environment.CurrentDirectory}\\Database\\";
        private string fileName = "Yahtzee";
        public string SaveToFile(DateTime date, int roundNumber, List<Player> players) 
        {
            string dateStr = date.ToString();
            dateStr = dateStr.Substring(2, 2) + dateStr.Substring(5, 2) + dateStr.Substring(8, 2) + dateStr.Substring(11, 2) + dateStr.Substring(14, 2) + dateStr.Substring(17, 2) + ".txt";

            StreamWriter file = new StreamWriter(path + fileName + dateStr);

            string output = date.ToString();
            output = date.ToString();
            file.WriteLine(output);
            output = roundNumber.ToString();
            file.WriteLine(output);
            foreach (Player p in players)
            {
                output = p.Name;
                file.WriteLine(output);
                output = p.IsAI.ToString().ToLower();
                file.WriteLine(output);
                Score[] score = p.GetScoreList();
                for (int i=0; i < score.Length; i++)
                {
                    output = "";
                    output += score[i].Points + "|" + score[i].UsedCombo;
                    file.WriteLine(output);
                }
            }
            file.Close();
            return path + fileName + dateStr;
        }
        public List<Player> GetFromFile(Rules rules, string fileName, out DateTime date, out int roundNumber)
        {
            string line;
            List<Player> players = new List<Player>();
            List<string> items = new List<string>();
            StreamReader file = new StreamReader(path + fileName);

            while((line = file.ReadLine()) != null)
            {
                items.Add(line);
            }
            file.Close();

            date = Convert.ToDateTime(items[0]);
            roundNumber = int.Parse(items[1]);

            string name = "";
            bool isAI = false;
            int rowsForPlayer = roundNumber + 2;
            int noOfPlayers = (items.Count - 2) / (roundNumber + 2);
            int indexStartPlayer = 2;
            for (int i = 0; i < noOfPlayers ;i++)
            {
                indexStartPlayer = 2 + i * rowsForPlayer;
                List<Score> scoreList = new List<Score>();
                name = items[indexStartPlayer];
                isAI = bool.Parse(items[indexStartPlayer + 1]);
                string[] scoreItems;

                for (int j = 0; j < roundNumber; j++)
                {
                    scoreItems = items[indexStartPlayer + 2 + j].Split('|');
                    int point = Int32.Parse(scoreItems[0]);
                    Combo c = (Combo)Enum.Parse(typeof(Combo), (scoreItems[1]));
                    Score score = new Score(c, point);
                    scoreList.Add(score);
                }
                if (isAI)
                {
                    AI ai = new AI(name, rules, scoreList);
                    players.Add(ai);
                }
                else
                {
                    Player player = new Player(name, scoreList);
                    players.Add(player);
                }
            }
            return players;
        }
        public FileInfo[] ListSavedGames()
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles("*.txt");
            return files;
        }
        public Database()
        {
            Directory.CreateDirectory(path);
        }
    }
}