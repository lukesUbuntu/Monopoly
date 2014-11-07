using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
    public class Transport : TradeableProperty
    {

        public Transport() : this("Railway Station"){}

        public Transport(string sName, IEnum.PropertyGroup dGroup = IEnum.PropertyGroup.NONE)
        {
            this.sName = sName;
            this.dPrice = 200;
            this.dMortgageValue = 100;
            this.dRent = 25;
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
                int ownedOfGroup = this.ownsHowMany(this);
                decimal pays = this.getRent();
                return string.Format("{0} owns {1}, rent is ${2}", this.getOwner(), ownedOfGroup, pays);
            }
            else
                return base.landOn(ref player);
        }

        public override decimal getRent()
        {

            //Calculates rent multiplied by how many player owns
             /* eg :    1 RR: $25
                        2 RR: $50
                        3 RR: $100
                        4 RR: $200*/
            decimal hasToPay = this.dRent;
            //Grab owners props for this group
            int ownedOfGroup = this.ownsHowMany(this);
            for (int payCount = 1; payCount < ownedOfGroup; payCount++)
            {
                hasToPay = hasToPay * 2;
            }

            return hasToPay;
        }
    }
}
