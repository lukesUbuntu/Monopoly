using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
    /// <summary>
    /// Handles the creating a luck
    /// </summary>
    public class LuckFactory : PropertyFactory
    {
        public Luck create(string sName, bool isPenalty, decimal dAmount, Game.CardType luckType = Game.CardType.None)
        {
            
            return new Luck(sName, isPenalty, dAmount, luckType);
        }



    }
}
