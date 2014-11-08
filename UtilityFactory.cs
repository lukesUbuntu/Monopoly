using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
    public class UtilityFactory : PropertyFactory
    {
        public Utility create(string sName, Game.PropertyGroup dGroup = Game.PropertyGroup.NONE)
        {
            
            return new Utility(sName, dGroup);
        }
    }
}
