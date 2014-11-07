using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
    public class LuckFactory : PropertyFactory
    {
        public Luck create(string sName, bool isPenalty, decimal dAmount, IEnum.Game luckType = IEnum.Game.None)
        {
            return new Luck(sName, isPenalty, dAmount, luckType);
        }
    }
}
