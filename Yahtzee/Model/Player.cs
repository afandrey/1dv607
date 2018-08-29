using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Player
    {
        private List<Score> scoreList;
        public string Decision { get; protected set; }
        public string Name { get; private set; }
        public bool IsAI { get; private set; }

        public void AddScore(Combo combo, int point)
        {
            scoreList.Add(new Score(combo, point));
        }
        public int GetScore(Combo combo, out bool exist)
        {
            Score s = scoreList.Find(scoreObj => scoreObj.UsedCombo == combo);

            if (s != null)
            {
                exist = true;
                return s.Points;
            }
            else
            {
                exist = false;
                return 0;
            }
        }
        public Score[] GetScoreList()
        {
            Score[] scoreListCopy = new Score[scoreList.Count];

            scoreList.CopyTo(scoreListCopy, 0);
            return scoreListCopy;
        }
        public int GetTotalScore()
        {
            int sum = 0;
            foreach (Score s in scoreList)
            {
                sum += s.Points;
            }
            return sum;
        }
        public bool GetUsedCombo(Combo combo)
        {
            Score s = scoreList.Find(scoreObj => scoreObj.UsedCombo == combo);
            if (s != null)
            {
                return true;
            }
            return false;
        }
        public List<Combo> GetUsedCombinations()
        {
            List<Combo> unavailableCombos = new List<Combo>();
            foreach (Combo c in ComboModel.GetList())
            {
                Score score = scoreList.Find(scoreObj => scoreObj.UsedCombo == c);
                if (score != null)
                {
                    unavailableCombos.Add(c);
                }
            }
            return unavailableCombos;
        }
        public Player()
        {
            this.scoreList = new List<Score>();
        }
        public Player(string name, bool ai = false) : this()
        {
            Name = name;
            IsAI = ai;
        }
        public Player(string name, List<Score> scores, bool ai = false) : this(name, ai)
        {
            foreach (Score s in scores)
            {
                scoreList.Add(s);
            }
        }
    }
}