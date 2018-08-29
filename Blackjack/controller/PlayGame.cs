using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJack.model;

namespace BlackJack.controller
{
    public class PlayGame : model.Observer
    {
        private model.Game m_game;
        private view.IView m_view;

        public bool Play(model.Game a_game, view.IView a_view)
        {
            m_view = a_view;
            m_game = a_game;

            m_view.DisplayWelcomeMessage();

            m_game.AddSubToPlayer(this);
            
            m_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            m_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());

            if (m_game.IsGameOver())
            {
                m_view.DisplayGameOver(m_game.IsDealerWinner());
            }

            view.MenuOptions input = m_view.GetInput();

            if (view.MenuOptions.PLAY.Equals(input))
            {
                m_game.NewGame();
            }
            else if (view.MenuOptions.HIT.Equals(input))
            {
                m_game.Hit();
            }
            else if (view.MenuOptions.STAND.Equals(input))
            {
                m_game.Stand();
            }

            return !view.MenuOptions.QUIT.Equals(input);
        }

        public void CardDealt()
        {
            m_view.Pause(1000);
            m_view.DisplayWelcomeMessage();
            m_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            m_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());
            m_view.Pause(1000);
        }
    }
}
