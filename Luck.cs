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
           
            //we have landed on chance or community card
            if (this.luckType != Game.CardType.None)
            {
               // String cardDetails = "";

                switch (this.luckType)
                {
                    case Game.CardType.CommunityChest :
                        //cardDetails = CommunityCards.access().draw_card(ref player);
                        return base.landOn(ref player) + String.Format("\n{0} Drawing Community Card...\n<color:Yellow>Card Reads</color> : {1}", player.getName(), CommunityCards.access().draw_card(ref player));
                    case Game.CardType.Chance:
                        //cardDetails = ChanceCards.access().draw_card(ref player);
                        return base.landOn(ref player) + String.Format("\n{0} Drawing Chance Card...\n<color:Yellow>Card Reads</color> : {1}", player.getName(), CommunityCards.access().draw_card(ref player));

                    default :
                   return base.landOn(ref player) + "Not implmented";
                }
                //@todo apply land on to community card
               

            }
            
            if (this.isBenefitNotPenalty)
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
