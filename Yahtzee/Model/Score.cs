using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Yahtzee_1dv607
{
    class Score 
    {
        public Combo UsedCombo { get; private set; }
        public int Points { get; private set; }
        public Score(Combo combo, int points)
        {
            UsedCombo = combo;
            Points = points;
        }
    }
}