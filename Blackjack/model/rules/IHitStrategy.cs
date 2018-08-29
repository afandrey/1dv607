using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    public interface IHitStrategy
    {
        bool DoHit(model.Player a_dealer);
    }
}
