using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    public class SoftSeventeenStrategy : IHitStrategy
    {
        private const int g_hitLimit = 17;

        public bool DoHit(model.Player a_dealer)
        {
            int score = a_dealer.CalcScore();
            bool containsAce = false;

            if (score >= g_hitLimit && score < 21)
            {
                containsAce = a_dealer.ContainsAces();
            }

            return score < g_hitLimit || containsAce;
        }
    }
}