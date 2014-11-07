using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{
    public class TradeableProperty : Property //should be abstract but not to make testing easier
    {
        protected decimal dPrice;
        protected decimal dMortgageValue;
        protected decimal dRent;
        //protected IEnum.PropertyGroup group;
        //decimal dMortgageValue;
        public TradeableProperty()
        {
            this.dPrice = 200;
            this.dMortgageValue = 100;
            this.dRent = 50;
        }

        public decimal getPrice()
        {
            return dPrice;
        }

        public virtual decimal getRent()
        {
            return this.dRent;
        }

        public virtual void payRent(ref Player player)
        {
            //check if the player has all prop groups for this
           
            decimal theRent = this.getRent();

            //player has complete group
            if (this.ownsAllProps(this))
            {
                theRent = theRent * 2;
            }

            player.pay(theRent);
            this.getOwner().receive(theRent);
        }

        public void purchase(ref Player buyer)
        {
            //check that it is owned by bank
            if (this.availableForPurchase())
            {
                //pay price 
                buyer.pay(this.getPrice());
                //set owner to buyer
                this.setOwner(ref buyer);
            }
            else
            {
                throw new ApplicationException("The property is not available from purchase from the Bank.");
            }
        }

        public override bool availableForPurchase()
        {
            //if owned by bank then available
            if (this.owner == Banker.access())
                return true;
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string landOn(ref Player player)
        {
            //Pay rent if needed & if prop is not morgaged
            if ((this.getOwner() != Banker.access()) && (this.getOwner() != player) )
            {
                if (this.isMortgaged())
                {
                    return base.landOn(ref player) + string.Format("Property {0} is currently morgaged skipping rent payment", this.getName());
                }
                //@todo inform user that owner has all props
                this.payRent(ref player);
                decimal the_rent = this.getRent();
                decimal paid_rent = (this.ownsAllProps(this)) ? the_rent * 2 : the_rent;

                return base.landOn(ref player) + string.Format("Rent has been paid for {0} of ${1} to {2}.", this.getName(), paid_rent, this.getOwner().getName());
            }
            else
                return base.landOn(ref player);
        }


        

        public decimal unMortgageProperty()
        {

            decimal payable = this.dMortgageValue + (this.dMortgageValue * 10 / 100);

            this.getOwner().pay(payable);
            Banker.access().receive(payable);

            this.mortgaged = false;

            return payable;
        }

        public override bool isMortgaged()
        {
            return this.mortgaged;
        }


        public override decimal mortgageProperty()
        {
            this.getOwner().pay(this.dMortgageValue);
            Banker.access().pay(this.dMortgageValue);
            this.mortgaged = true;

            return this.dMortgageValue;
        }



        public override ArrayList returnGroupProperties(Property theProperty)
        {
            //Store temp props
            ArrayList tmpProps = new ArrayList();

            int total_group = 0;
            int owned_group = 0;
            IEnum.PropertyGroup theGroup = theProperty.getGroup();

            //go through all the properties
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                if (Board.access().getProperty(i).getGroup() == theGroup)
                    total_group++;

                //owned by this player
                Property thisProperty = Board.access().getProperty(i);
                if (thisProperty.getOwner() == this.getOwner())
                {
                    //add to arraylist
                    tmpProps.Add(thisProperty);
                    owned_group++;
                }
            }
            //return propertiesOwned;

            //owns all groups of prop
            if (tmpProps.Count == total_group)
            {
                return tmpProps;
            }

            return null;

        }

        public bool ownsAllProps(Property theProperty)
        {
            return (returnGroupProperties(theProperty) != null);
        }


        
       
    }
}
