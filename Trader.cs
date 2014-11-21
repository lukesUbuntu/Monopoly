using System;
using System.Collections;
using System.Text;

namespace MonopolyGame_9901623
{
    
    /// <summary>
    /// Class that represents a trader that can transfer money and own properties
    /// </summary>
    
    public class Trader //should be abstract but not to make testing easier
    {
        protected ArrayList propertiesOwned = new ArrayList();
        protected decimal dBalance; 
        protected string sName;


        public Trader(){ }

        //constructor with name and balance
        public Trader(string sName, decimal dBalance)
        {
            this.sName = sName;
            this.dBalance = dBalance;
        }
        

        public void receive(decimal dAmount)
        {
            this.dBalance += dAmount;
        }

        public void pay(decimal dAmount)
        {
            this.dBalance -= dAmount;
            checkBankrupt();
        }

        public virtual void checkBankrupt()
        {
            if (this.getBalance() <= 0)
                throw new ApplicationException(String.Format("{0} is Bankrupt", this.getName()));
        }
                
        

        public override string ToString()
        {
            return String.Format("Name: {0} \nBalance: {1}", this.sName, this.dBalance);
        }

        public String getName()
        {
            return this.sName;
        }

        public void setName(String sName)
        {
            this.sName = sName;
        }

        public void setBalance(decimal dBalance)
        {
            this.dBalance = dBalance;
        }

        public decimal getBalance()
        {
            return this.dBalance;
        }

        public void obtainProperty(ref Property property)
        {
            this.propertiesOwned.Add(property);
        }
       /// <summary>
       /// Trades property with another player, also will pay morgage if prop is morgaged
       /// </summary>
       /// <param name="property">the player who has property</param>
       /// <param name="purchaser">new player </param>
       /// <param name="amount">amount want for propert</param>
        public void tradeProperty(ref TradeableProperty property, ref Player purchaser, decimal amount)
        {
           

            purchaser.pay(amount);
            this.receive(amount);
            property.setOwner(ref purchaser);

            if (property.isMortgaged())
                property.unMortgageProperty();
            
        }
        //internal
        public  ArrayList getPropertiesOwned()
        {
            return this.propertiesOwned;
        }

       
    }
}
