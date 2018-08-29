using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class ViewController
    {
        private ScoreView scoreView;
        private SetupView setupView;
        private RoundView roundView;
        public int NumberOfPlayers()
        {
            return setupView.NumberOfPlayers();
        }
        public string SetPlayerName(int number, out bool ai)
        {
            string name = "";

            ai = setupView.IsAI(number);
            if (!ai)
            {
                name = setupView.GetPlayerName(number);
            }
            Console.Clear();
            return name;
        }
        public void RenderRoundNumber(int roundNumber)
        {
            roundView.RenderRoundNumber(roundNumber);
        }
        public void RenderRound(string name)
        {
            roundView.RenderRound(name);

        }
        public bool[] GetDiceToRoll()
        {
            return roundView.GetDiceToRoll();
        }
        public void RenderDice(int[] dice)
        {
            roundView.RenderDice(dice);
        }
        public void RenderUnavailableCombos(List<Combo> unavailableCombos)
        {
            if (roundView.SelectActivity(DisplayType.ViewAvaialbleCombos, false))
            {
                roundView.RenderUnavailableCombos(unavailableCombos);
            }
        }
        public Combo RenderCombo(List<Combo> unavailableCombos)
        {
            return roundView.RenderCombo(unavailableCombos);
        }
        public void RenderDiceToRoll(bool[] DiceToRoll, string decision = "")
        {
            roundView.RenderDiceToRoll(DiceToRoll, decision);
            Thread.Sleep(2000);
        }
        public void GetRoundScore(int roundScore, Combo usedCombo)
        {
            scoreView.GetRoundScore(roundScore, usedCombo);
        }
        public bool ContinueGame()
        {
            return roundView.ContinueGame();
        }
        public bool ResumeGame()
        {
            return roundView.SelectActivity(DisplayType.ResumeSavedGame);
        }
        public bool ViewVerboseList()
        {
            return roundView.SelectActivity(DisplayType.ViewVerboseList);
        }
        public bool ViewGameResult()
        {
            return roundView.SelectActivity(DisplayType.ViewSavedGame);
        }
        public void SaveGame(string fileName)
        {
            roundView.SaveGame(fileName);
        }
        public void FinishedGame(string name, int score)
        {
            roundView.FinishedGame(name, score);
        }
        public void RenderScoreBoard(List<Player> players, string date = null, bool verboseList = true)
        {
            scoreView.RenderScoreBoard(players, date, verboseList);
        }
        public string GetSavedGame(FileInfo[] files)
        {
            return roundView.GetSavedGame(files);
        }
        public ViewController()
        {
            setupView = new SetupView();
            scoreView = new ScoreView();
            roundView = new RoundView();
        }
    }
}