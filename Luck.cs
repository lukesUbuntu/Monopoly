using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MonopolyGame_9901623
{

    
    public class Luck : Property 
    {
        bool isBenefitNotPenalty;
        decimal penaltyOrBenefitAmount;
        Game.CardType luckType;
        //private CommunityManager theManager;

        public Luck() : this("Luck Property", true, 50) { }

        public Luck(string sName, bool isBenefitNotPenalty, decimal amount, Game.CardType luckType = Game.CardType.None)
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
            if (this.luckType == Game.CardType.CommunityChest)
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
