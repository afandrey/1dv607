using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    public class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_softRule;
        private rules.IWinnerStrategy m_winRule;

        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_softRule = a_rulesFactory.SoftSeventeenRule();
            m_winRule = a_rulesFactory.PlayerWinsRule();
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(m_deck, this, a_player);
            }
            return false;
        }

        public void Deal(Player a_player, bool show)
        {
            Card c = m_deck.GetCard();
            c.Show(show);
            a_player.DealCard(c);
            m_observer.CardDealt();
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                this.Deal(a_player, true);

                return true;
            }
            return false;
        }

        public bool Stand()
        {
            if (m_deck != null)
            {
                this.ShowHand();

                while (this.m_softRule.DoHit(this))
                {
                    this.Deal(this, true);
                }
                return true;
            }
            return false;
        }

        public bool IsDealerWinner(Player a_player)
        {
            return m_winRule.IsDealerWinner(this, a_player);
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /*CalcScore() >= g_hitLimit*/ m_softRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }
    }
}
