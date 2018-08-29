using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJack.controller;
using BlackJack.view;

namespace BlackJack.model
{
    public interface Observer
    {
        void CardDealt();
    }
}