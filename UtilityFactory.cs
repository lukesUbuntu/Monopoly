using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
    public class UtilityFactory : PropertyFactory
    {
        public Utility create(string sName, IEnum.PropertyGroup dGroup = IEnum.PropertyGroup.NONE)
        {
            return new Utility(sName, dGroup);
        }
    }
}
