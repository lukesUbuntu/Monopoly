using System;
using System.Collections;
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


        public override string landOn(ref Player player)
        {
            //Pay rent if needed
            if ((this.getOwner() != Banker.access()) && (this.getOwner() != player))
            {
                //How many 
                int tmpProps = this.ownsHowMany(this);
                //pay rent
                //this.payRent(ref player);
                return string.Format("You rolled a total of ");
            }
            else
                return base.landOn(ref player);
        }
    }
}
