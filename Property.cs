using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
{

    public class Property
    {
        protected string sName;
        protected Trader owner;
        protected IEnum.PropertyGroup group;
        protected bool mortgaged;
        decimal dMortgageValue;

        protected decimal dHouseCost;
        public Property(): this("Property"){}

        public Property(string sName)
        {
            this.sName = sName;
            this.owner = Banker.access();
            
        }


        public Property(string sName, ref Trader owner, decimal dMortgageValue)
        {
            this.sName = sName;
            this.owner = owner;
            this.dMortgageValue = dMortgageValue;
        }
        public Trader getOwner()
        {
            return this.owner;
        }

        public void setOwner(ref Banker newOwner)
        {
            this.owner = newOwner;
        }

        public void setOwner(ref Player newOwner)
        {
            this.owner = newOwner;
        }

        public string getName()
        {

            //IEnum.PropertyGroup question = Question.Role;
            //int value = (int)question;

            return "<color:" + this.getColor() + ">" + this.sName + "</color>";
        }

        public ConsoleColor getColor()
        {
            //int Question = (int)this.group;
            return (ConsoleColor)(int)this.getGroup();
        }

        public IEnum.PropertyGroup getGroup()
        {
            //Returns the current group as string
            
            return this.group;
        }
        public virtual string landOn(ref Player player)
        {
            return String.Format("{0} landed on {1}. ", player.getName(), this.getName());
        }

        public override string ToString()
        {
            return String.Format("{0}:\tOwned by: {1}", this.getName(), this.getOwner().getName());
        }

        public virtual bool availableForPurchase()
        {
            return false;//generic properties are not available for purchase
        }


        public bool ownsAllProps(Property theProperty)
        {
           return (returnGroupProperties(theProperty) != null);
        }


        public virtual ArrayList returnGroupProperties(Property theProperty)
        {
            ArrayList tmpProps = new ArrayList();
            return tmpProps;
        }
       

        public virtual bool isMortgaged()
        {
            return this.mortgaged;
        }


        public virtual decimal mortgageProperty()
        {
            return this.dMortgageValue / 2;
        }
    }

   
}
