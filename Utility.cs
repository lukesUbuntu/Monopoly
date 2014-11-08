using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
     
    public class Utility : TradeableProperty
    {
        static int rentMultiplied = 6; //factor to multiply times roll of dice to getRent

        public Utility() : this("Utility"){}

        public Utility(String name, Game.PropertyGroup dGroup = Game.PropertyGroup.NONE)
        {
            this.sName = name;
            this.owner = Banker.access();
            this.group = dGroup;
        }

        public override string ToString()
        {
           return base.ToString();
        }
        public override void payRent(ref Player player)
        {
            player.pay(this.getRent(ref player));
            this.getOwner().receive(this.getRent());
        }

        public decimal getRent(ref Player player)
        {
            return (this.rentMultiplier() * player.getLastMove());
        }

        public int rentMultiplier()
        {
            return rentMultiplied * this.ownsHowMany(this);
        }

        public override string landOn(ref Player player)
        {
            //Pay rent if needed
            if ((this.getOwner() != Banker.access()) && (this.getOwner() != player))
            {
                
                //pay rent
                this.payRent(ref player);
                return string.Format("You rolled a total of {0}. So your rent is {0} x {1} = ${2}.", player.getLastMove(), this.rentMultiplier(), (player.getLastMove() * this.rentMultiplier()));
            }
            else
                return base.landOn(ref player);
        }

        
    }
    
}
