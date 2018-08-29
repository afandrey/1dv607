using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Game
    {
        private Database db;
        private List<Player> players;
        private Rules rules;
        private DiceList diceList;
        private ViewController view;
        private DateTime Date { get; set; }
        private int RoundNumber { get; set; }
        private bool[] DiceToRoll { get; set; }
        private void InitGame()
        {
            db = new Database();
            diceList = new DiceList();
            rules = new Rules(diceList);
            view = new ViewController();

            string resumeGameStr = "";
            string viewGameStr = "";
            while (view.ViewGameResult())
            {
                FileInfo[] files = db.ListSavedGames();
                viewGameStr = view.GetSavedGame(files);
                if (viewGameStr != "")
                {
                    ViewGame(viewGameStr);
                }
            }
            if (view.ResumeGame())
            {
                FileInfo[] files = db.ListSavedGames();
                resumeGameStr = view.GetSavedGame(files);
            }
            if (resumeGameStr != "")
            {
                ResumeGame(resumeGameStr);
            }
            else
            {
                Date = DateTime.Now;
                RoundNumber = 0;
                PlayerSetup();
            }
        }
        private void ViewGame(string viewGame)
        {
            List<string> items = new List<string>();
            DateTime date = new DateTime();
            bool verbostList = view.ViewVerboseList();
            int roundNumber = 0;
            players = db.GetFromFile(rules, viewGame, out date, out roundNumber);

            Date = date;
            RoundNumber = roundNumber;
            view.RenderScoreBoard(players, date.ToString(), verbostList);
        }
        private void ResumeGame(string resumeGame)
        {
            DateTime date = new DateTime();
            int roundNumber = 0;
            players = db.GetFromFile(rules, resumeGame, out date, out roundNumber);
            Date = date;
            RoundNumber = roundNumber;
        }
        private void PlayerSetup()
        {
            bool ai;
            players = new List<Player>();
            int numberOfPlayers = view.NumberOfPlayers();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                string name = view.SetPlayerName(i, out ai);
                if (ai)
                {
                    players.Add(new AI(GetNumberOfAIs() + 1, rules));

                }
                else
                {
                    players.Add(new Player(name));
                }

            }
        }
        private void RunGame()
        {
            string fileName = "";
            int startRound = RoundNumber+1;
            for (int i = startRound; i <= ComboModel.GetSize(); i++)
            {
                if (i != startRound && !view.ContinueGame())
                {
                    fileName = db.SaveToFile(Date, RoundNumber, players);
                    view.SaveGame(fileName);
                    return;
                }
                RunRound(i);
                RoundNumber++;
            }
            EndGame();
        }
        private void RunRound(int roundNumber)
        {
            view.RenderRoundNumber(roundNumber);
            foreach (Player p in players)
            {
                DiceToRoll = new bool[] { true, true, true, true, true };
                PlayRound(p);
            }
            view.RenderScoreBoard(players);
        }
        private void PlayRound(Player player)
        {
            AI ai = player as AI;
            Combo comboToUse = Combo.Chance;

            view.RenderRound(player.Name);
            for (int rollNumber = 1; rollNumber <= 3; rollNumber++)
            {
                if (AnyDiceToRoll())
                {
                    diceList.Roll(DiceToRoll);
                    view.RenderDice(diceList.GetDice());
                    if (rollNumber < 3)
                    {
                        if (player.IsAI)
                        {
                            DiceToRoll = ai.GetDiceToRoll(diceList.GetNumberOfDiceFaceValue(), diceList.GetDice());
                        }
                        else
                        {
                            if (rollNumber == 1)
                                view.RenderUnavailableCombos(player.GetUsedCombinations());
                            DiceToRoll = view.GetDiceToRoll();
                        }
                        view.RenderDiceToRoll(DiceToRoll, player.Decision);
                    }
                }
            }
            if (player.IsAI)
            {
                comboToUse = ai.SelectComboToUse();
            }
            else
            {
                comboToUse = view.RenderCombo(player.GetUsedCombinations());
            }
            player.AddScore(comboToUse, rules.CheckCombinations(comboToUse));

            bool exist = false;
            int roundScore = player.GetScore(comboToUse, out exist);
            if (exist)
            {
                view.GetRoundScore(roundScore, comboToUse);
            }
        }
        private void EndGame()
        {
            string fileName = db.SaveToFile(Date, RoundNumber, players);
            int highScore = 0;
            string winner = "";
            foreach (Player p in players)
            {
                if (p.GetTotalScore() == highScore)
                {
                    winner = "We have a draw";
                    highScore = p.GetTotalScore();
                }
                if (p.GetTotalScore() > highScore)
                {
                    winner = p.Name;
                    highScore = p.GetTotalScore();
                }
            }
            view.FinishedGame(winner, highScore);
            view.SaveGame(fileName);
        }
        private bool AnyDiceToRoll()
        {
            bool roll = false;
            for (int i=0; i< DiceToRoll.Length;i++)
            {
                if (DiceToRoll[i])
                    roll = true;
            }
            return roll;
        }
        private int GetNumberOfAIs()
        {
            int numberOfAIs = 0;
            foreach (Player p in players)
            {
                if (p.IsAI)
                {
                    numberOfAIs++;
                }
            }
            return numberOfAIs;
        }
        public Game()
        {
            InitGame();
            RunGame();
        } 
    }
}