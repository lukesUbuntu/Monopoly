using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MonopolyGame_9901623
{
   
    /// <summary>
    /// Class for players playing monopoly
    /// </summary>
    
    public class Player : Trader
    {
        private int location;
        private int lastMove;

        private int allMoves;
        private bool playerAction = false;
        //Jail globs
        //has rolled already rolled to get out of jail
        private bool jailRolledAlready;
        private bool inJail;
        private bool getOutOfJailCard = false;
        private int rolledDoubles = 0;
        private int rolledDoublesOutOfJailCounter = 0;
        //each player has two dice
        Die die1 = new Die();

        //rolling with 1 dice while testing also check method move()
        Die die2 = new Die(); 

        bool isInactive = false;

        //event for playerBankrupt
        public event EventHandler playerBankrupt;
        public event EventHandler playerPassGo;

        public Player()
        {
            this.sName = "Player";
            this.dBalance = InitialValuesAccessor.getPlayerStartingBalance();
            this.location = 0;
            this.inJail = false;
            this.jailRolledAlready = false;
            this.getOutOfJailCard = false;
        }

        public Player(string sName)
        {
            this.sName = sName;
            this.dBalance = InitialValuesAccessor.getPlayerStartingBalance();
            this.location = 0;
        }


        public Player(string sName, decimal dBalance) : base(sName, dBalance)
        {
            this.location = 0;
        }
        /// <summary>
        /// Returns if the player has get out of jail card
        /// </summary>
        /// <returns>bool</returns>
        public bool hasGetOutJailCard(){
            return this.getOutOfJailCard;

        }
        /// <summary>
        /// sets if the player has completed an action is set to false by default
        /// </summary>
        /// <param name="madeAction">bool</param>
        public void setPlayerActionCompleted(bool madeAction = false)
        {
            this.playerAction = madeAction;
        }
        /// <summary>
        /// returns if the player has complete a action
        /// </summary>
        /// <returns></returns>
        public bool playerMadeAction()
        {
            return this.playerAction;
        }
        /// <summary>
        /// Sets the player has a get out of jail card
        /// </summary>
        public void giveGetOutJailCard()
        {
            this.getOutOfJailCard = true;
        }
        /// <summary>
        /// returns true or false if player has rolled more than 3 doubles in a row
        /// </summary>
        /// <returns>bool</returns>
        public bool rolled3inRow()
        {
            return (rolledDoubles >= 3);
        }
        /// <summary>
        /// Sets a dice has been rolled double if player was the last player to roll double else resest the double count
        /// </summary>
        private void setRolledDoubles()
        {
            //if this is our first roll of doubles we must clear the counter
            if (Board.access().getLastPlayer() != this)
            {
                rolledDoubles = 1;
            }
            else
            {
                rolledDoubles++;
            }

        }
        /// <summary>
        /// returns true or false if the player has rolled double
        /// </summary>
        /// <returns>bool</returns>
        public bool rolledDouble(){
            return (die1.numberLastRolled() == die2.numberLastRolled());
        }
        public void move()
        {
            
            die1.roll();
            die2.roll();

            //move distance is total of both throws
            int iMoveDistance = die1.numberLastRolled() + die2.numberLastRolled();
               //+ die2.roll();
            //increase location
           this.setLocation(this.getLocation() + iMoveDistance);
           this.lastMove = iMoveDistance;

            //store int of all moves this is so we know if we have completed a complete trip around the board
           this.allMoves += this.lastMove;

            //if we have rolled doubles while still our turn
            if (this.rolledDouble()){
                setRolledDoubles();
            }
            

        }
        /// <summary>
        /// rolls for the jail
        /// </summary>
        /// <returns></returns>
        public string jailRollDice()
        {
            if (this.jailRolledAlready)
            {
                return String.Format("Already rolled \t<color:Blue>Dice 1</color>: {0}\t<color:Green>Dice 2</color>: {1}", die1.numberLastRolled(), die2.numberLastRolled());
            }
            //Roll dice to get out of jail
            die1.roll();
            die2.roll();

            if (die1.numberLastRolled() == die2.numberLastRolled())
            {
                this.inJail = false;
               
            }

            //we have rolled lets confirm this so user doesn't try to re-roll
            this.rolledDoubles = 0;
            this.jailRolledAlready = true;
            rolledDoublesOutOfJailCounter++;
            return String.Format("Rolling Dice:\t<color:Blue>Dice 1:</color> {0}\t<color:Green>Dice 2:</color> {1} {2}", die1.numberLastRolled(), die2.numberLastRolled(), (this.inJail) ? "" : "Rolled double out of jail");
   
        }
        /// <summary>
        /// returns true or false if player has reached max roll out of jail
        /// </summary>
        /// <returns>bool</returns>
        public bool rolledMaxDoublesOutOfJail()
        {
            return (rolledDoublesOutOfJailCounter >= 3);
        } 
        public int getLastMove()
        {
            return this.lastMove;
        }

        public string BriefDetailsToString()
        {
            return String.Format("You are on {0}.\tYou have <color:White>$</color><color:Yellow>{1}</color>.", Board.access().getProperty(this.getLocation()).getName(), this.getBalance());
        }

        public override string ToString()
        {
            return this.getName();
        }

        public string FullDetailsToString()
        {
            return String.Format("Player:{0}.\n Balance: <color:White>$</color><color:Yellow>{1}</color>\nLocation: {2} (Square {3}) \nProperties Owned:\n{4}", 
                                this.getName(), this.getBalance(), Board.access().getProperty(this.getLocation()), this.getLocation(), this.PropertiesOwnedToString());
        }

        public string PropertiesOwnedToString()
        {
            string owned = "";
            //if none return none
            if (getPropertiesOwnedFromBoard().Count == 0)
                return "None";
            //for each property owned add to string owned
            for (int i = 0; i < getPropertiesOwnedFromBoard().Count; i++)
            {
                owned += getPropertiesOwnedFromBoard()[i].ToString() + "\n";
            }
            return owned;
        }

        public void setLocation(int location)
        {
           
            //if set location is greater than number of squares then move back to beginning
            if (location >= Board.access().getSquares())
            {
                location = (location - Board.access().getSquares());
                //raise the pass go event if subscribers
                if(playerPassGo != null)
                    this.playerPassGo(this, new EventArgs());
                //add 200 for passing go
                this.receive(200);
            }

            this.location = location;
        }

        public int getLocation()
        {
            return this.location;
        }

        public string diceRollingToString()
        {

            return String.Format("Rolling Dice:\t<color:Blue>Dice 1:</color> {0}\t<color:Green>Dice 2:</color> {1} {2}", die1, die2, (this.rolledDouble()) ? "<color:Red>You rolled doubles...</color>" : "");
            //return String.Format("Rolling Dice:\tDice 1: {0}", die1); 
        }
        /// <summary>
        /// Returns an arraylist of all properties owned by the owner (this) owner
        /// </summary>
        /// <returns>ArrayList</returns>
        public ArrayList getPropertiesOwnedFromBoard()
        {
            ArrayList propertiesOwned = new ArrayList();
            //go through all the properties
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                //owned by this player
                if (Board.access().getProperty(i).getOwner() == this)
                {

                    //add to arraylist
                    propertiesOwned.Add(Board.access().getProperty(i));
                }
            }
            return propertiesOwned;
        }

      
        /// <summary>
        /// If player owns all groups to the property this will then return all the properties in that group.
        /// If not it will return null
        /// </summary>
        /// <param name="theProperty">theProperty</param>
        /// <returns>ArrayList Property|null</returns>
        public ArrayList returnGroupProperties(Property theProperty)
        {
            //Store temp props
            ArrayList tmpProps = new ArrayList();

            int total_group = 0;
            int owned_group = 0;
            Game.PropertyGroup theGroup = theProperty.getGroup();

            //if the prop groups is none then return jus this property
            if (theGroup == Game.PropertyGroup.NONE)
            {
                 tmpProps.Add(theProperty);
                 return tmpProps;
            }

            //go through all the properties
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                if (Board.access().getProperty(i).getGroup() == theGroup)
                    total_group++;

                //owned by this player
                Property thisProperty = Board.access().getProperty(i);
                if (thisProperty.getOwner() == this)
                {
                    //add to arraylist
                    tmpProps.Add(thisProperty);
                    owned_group++;
                }
            }
            //return propertiesOwned;

            //owns all groups of prop
            if (tmpProps.Count == total_group){
                return tmpProps;
            }

            return null;

        }
        public override void checkBankrupt()
        {
            if (this.getBalance() <= 0)
            {
                //raise the player bankrupt event if there are subscribers
                if (playerBankrupt != null)
                    this.playerBankrupt(this, new EventArgs());

                //return all the properties to the bank
                Banker b = Banker.access();
              
                foreach (Property p in this.getPropertiesOwnedFromBoard())
                {
                    p.setOwner(ref b);
                }
                //set isInactive to true
                this.isInactive = true;


            }
        }

        public bool isNotActive()
        {
            return this.isInactive;
        }
        public bool passGo()
        {
            return (this.allMoves > 40);
        }

        /// <summary>
        /// puts player into jail , also finds the jail location on board
        /// </summary>
        public void setIsInJail()
        {
            //this.location = 11;
            //we need to find where jail is on board
            int jailLocation = 0;
            ArrayList theProps = Board.access().getProperties();
            for (int location = 0; location < theProps.Count; location++) 
            {
                if (Board.access().getProperty(location) is Jail)
                {
                    if (((Jail)theProps[location]).isJailProp() == false)
                    {
                        jailLocation = location;
                        break;
                    }
                }
            }
            this.rolledDoubles = 0;
            this.location = jailLocation;
            this.inJail = true;
        }

        /// <summary>
        /// Returns true or fals if the player is in jail
        /// </summary>
        /// <returns>bool</returns>
        public bool getIsInJail()
        {
            return this.inJail;
        }

        /// <summary>
        /// After user has rolled this will reset the roll
        /// </summary>
        public void resetJailRoll()
        {
            this.jailRolledAlready = false;
        }
        /// <summary>
        /// This will pay the $50 fine to the banker from player
        /// </summary>
        public void payJailFee()
        {
            //pay jail fee
            this.pay(50);
            Banker.access().receive(50);
            this.inJail = false;
            this.setLocation(this.location);
        }

        /// <summary>
        /// Uses jail card to get out of jail also returns the jail card to the card deck
        /// </summary>
        public void useJailCard()
        {
            if (this.hasGetOutJailCard())
            {
                CommunityCards.access().return_jail_card();
                this.inJail = false;
                this.getOutOfJailCard = false;
                this.setLocation(this.location);
            }

            
        }
    }
}
