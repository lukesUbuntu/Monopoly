using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
    public class Transport : TradeableProperty
    {

        public Transport() : this("Railway Station"){}

        public Transport(string sName, IEnum.PropertyGroup dGroup = IEnum.PropertyGroup.NONE)
        {
            this.sName = sName;
            this.dPrice = 200;
            this.dMortgageValue = 100;
            this.dRent = 50;
            this.owner = Banker.access();
            this.group = dGroup;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
