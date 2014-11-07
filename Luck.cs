using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MolopolyGame
{

    
    public class Luck : Property 
    {
        bool isBenefitNotPenalty;
        decimal penaltyOrBenefitAmount;
        IEnum.Game luckType;
        //private CommunityManager theManager;

        public Luck() : this("Luck Property", true, 50) { }

        public Luck(string sName, bool isBenefitNotPenalty, decimal amount, IEnum.Game luckType = IEnum.Game.None)
        {
            this.sName = sName;
            this.isBenefitNotPenalty = isBenefitNotPenalty;
            this.penaltyOrBenefitAmount = amount;
            this.luckType = luckType;

        }
        public override string ToString()
        {
            return base.ToString();
        }

        public override string landOn(ref Player player)
        {
           
            //if is a benefit player receives amount else pay amount
            if (this.luckType == IEnum.Game.CommunityChest)
            {

                //@todo apply land on to community card
                CommunityManager.access().draw_card(ref player);
                return base.landOn(ref player) + String.Format("{0} has recieved {2}.", player.getName(), this.getName(), this.penaltyOrBenefitAmount);

            }else if (this.isBenefitNotPenalty)
            {
                player.receive(this.penaltyOrBenefitAmount);
                return base.landOn(ref player) + String.Format("{0} has recieved {2}.", player.getName(), this.getName(), this.penaltyOrBenefitAmount);
            }
            else
            {
                player.pay(this.penaltyOrBenefitAmount);
                return base.landOn(ref player) + String.Format("{0} has paid {2}.", player.getName(), this.getName(), this.penaltyOrBenefitAmount);
            }
        }
    }
}
