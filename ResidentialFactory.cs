using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
    public class ResidentialFactory : PropertyFactory
    {
        public Residential create(string sName, decimal dPrice, decimal dRent, decimal dHouseCost, Game.PropertyGroup dGroup = Game.PropertyGroup.NONE)
        {
            return new Residential(sName, dPrice, dRent, dHouseCost, dGroup);
        }
    }
}

