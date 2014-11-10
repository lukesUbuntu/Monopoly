using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
    public class Residential : TradeableProperty
    {

        
        int iHouses;
        static int iMaxHouses = 4;
       
        //int iHotels; //not implemented

        public Residential() : this("Residential Property"){}

        public Residential(String sName) : this(sName, 200, 50, 50) { }

        public Residential(String sName, decimal dPrice, decimal dRent, decimal dHouseCost, Game.PropertyGroup dGroup = Game.PropertyGroup.NONE)
        {
            this.sName = sName;
            this.dPrice = dPrice;
            this.dMortgageValue = dPrice / 2;
            this.dRent = dRent;
            this.dHouseCost = dHouseCost;
            this.group = dGroup;
            this.mortgaged = false;
            
        }

        

        public override decimal getRent()
        {
            //rent is rental amount plus the rental amount for each house
            return (dRent + (dRent * iHouses));
        }

        public void addHouses(int houses)
        {
            this.iHouses = houses;
        }

        public void addHouse()
        {
            // pay for houses
            this.getOwner().pay(this.dHouseCost);
            //add houses to residental
            this.iHouses ++;
        }

        public void addHotel()
        {
            if (this.iHouses >= iMaxHouses)
            {
                // pay for houses
                this.getOwner().pay(this.dHouseCost * 2);
                //add houses to residental
                this.iHouses++;
            }
           
        }
        /// <summary>
        /// Sells a house or hotel back to banker
        /// </summary>
        public void sellHouse(){
            decimal costPrice = this.dHouseCost / 2;
            //if has hotel double price
            if (this.hasHotel())
                costPrice = costPrice * 2;

            Banker.access().pay(costPrice);
            this.owner.receive(costPrice);
            //remove house
            this.iHouses--;
            
        }
        public Boolean hasHotel()
        {
            return (this.iHouses > getMaxHouses());
        }
        public decimal getHouseCost()
        {
            return this.dHouseCost;
        }

        public int getHouseCount()
        {
            return this.iHouses;
        }

        public static int getMaxHouses()
        {
            return iMaxHouses;
        }

        public override string ToString()
        {
            //String houseHotel = (this.hasHotel() == true) ? "Hotels : 1" : "Houses : "+this.getHouseCount().ToString();
            return base.ToString() + string.Format("\t : {0}", (this.hasHotel() == true) ? "Hotels : 1" : "Houses : " + this.getHouseCount().ToString());
        }
        /// <summary>
        /// Morgages property and gives money to player
        /// </summary>
        public override void mortgageProperty()
        {
            this.getOwner().receive(this.dMortgageValue);
            Banker.access().pay(this.dMortgageValue);
            this.mortgaged = true;

            //return this.dMortgageValue;
        }
        /// <summary>
        /// Returns the unmortgage price user will have to pay
        /// </summary>
        /// <returns>decimal value</returns>
        public override decimal unMortgagePropertyPrice()
        {
            return (this.dMortgageValue * 10 / 100) + this.dMortgageValue;
           
        }
        public override decimal mortgagePropertyPrice()
        {
            return this.dMortgageValue;
        }
       
      
    }
}

