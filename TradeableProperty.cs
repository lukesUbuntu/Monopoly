using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
    public class TradeableProperty : Property //should be abstract but not to make testing easier
    {
        protected decimal dPrice;
        protected decimal dMortgageValue;
        protected decimal dRent;
        protected decimal dGroup;
        //protected Game.PropertyGroup group;
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

        //@this gets overided
        /// <summary>
        /// Land on also calcalates the amount when the user has landed on
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
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


        
        /// <summary>
        /// Unmortages a propert and returns the decmial value paid to unmortage
        /// </summary>
        /// <returns>decimal paid</returns>
        public decimal unMortgageProperty()
        {
            //(this.dMortgageValue * 10 / 100) + this.dMortgageValue
            decimal payable = (this.dMortgageValue * 10 / 100) + this.dMortgageValue;

            this.getOwner().pay(payable);
            Banker.access().receive(payable);

            this.mortgaged = false;

            return payable;
        }
        /// <summary>
        /// Checks if property is mortgaged
        /// </summary>
        /// <returns>bool true or false</returns>
        public override bool isMortgaged()
        {
            return this.mortgaged;
        }
      
        
        //need to grab all the groups for that property and then return list to another method
        /// <summary>
        /// Returns a arraylist of all properties in a group for that property
        /// </summary>
        /// <param name="theProperty">The property</param>
        /// <param name="checkOwnsAll">to skip if owner owns prop</param>
        /// <returns>property list of all in a group</returns>
        public override ArrayList returnGroupProperties(Property theProperty,bool checkOwnsAll = true)
        {
            //Store temp props
            ArrayList tmpProps = new ArrayList();

            int total_group = 0;
            int owned_group = 0;
            Game.PropertyGroup theGroup = theProperty.getGroup();
            //there is no groups for this prop so return itself as only group
            if (theGroup == Game.PropertyGroup.NONE)
            {
                tmpProps.Add(theProperty);
                return tmpProps;
            }
            ArrayList propGroups = Board.access().getPropGroups(theGroup);

            //go through all the properties
            for (int i = 0; i < propGroups.Count; i++)
            {

                Game.PropertyGroup AlltheGroup = Board.access().getProperty(i).getGroup();
                if (AlltheGroup == theGroup)
                    total_group++;

                //owned by this player
                //Property thisProperty = Board.access().getProperty(i);
                //Property thisProperty = ((Property)propGroups[i]).getOwner();

                if (((Property)propGroups[i]).getOwner() == this.getOwner())
                {
                    //add to arraylist
                    tmpProps.Add(propGroups[i]);
                    owned_group++;
                }
            }
            //return propertiesOwned;

            //only runs if we are checking they have all props
            if (checkOwnsAll)
            {
                if (tmpProps.Count == total_group)
                {
                    return tmpProps;
                }

                if (tmpProps.Count <= 0) return null;
            }
           
            return tmpProps;

        }
        /// <summary>
        /// checks if they own all properties of a group returns true or false
        /// </summary>
        /// <param name="theProperty">Property</param>
        /// <returns>bool</returns>
        public override bool ownsAllProps(Property theProperty)
        {
            return (returnGroupProperties(theProperty) != null);
        }
        /// <summary>
        /// Returns the amount of properties in a group 
        /// </summary>
        /// <param name="theProperty">theProperty</param>
        /// <returns>int</returns>
        public override int ownsHowMany(Property theProperty)
        {
            //@todo need to test if the array is null what will it return;
            ArrayList theArray = returnGroupProperties(theProperty,false);
            int count = theArray.Count;

            return count;
        }
        /// <summary>
        /// gets the current group of this property
        /// </summary>
        /// <returns>Game.PropertyGroup</returns>
        public override Game.PropertyGroup getGroup()
        {
            //Returns the current group as string

            return this.group;
        }
        
       
    }
}
