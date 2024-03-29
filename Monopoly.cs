using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MonopolyGame_9901623
{

    /// <summary>
    /// Main class for monoploy game that implements abstract class game
    /// </summary>
    
    public class Monopoly : Game
    {

        ConsoleColor[] colors = new ConsoleColor[8] { 
            ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Gray, ConsoleColor.Blue, ConsoleColor.DarkYellow
        };
        
        bool gameSetUp = false;
        private string colorUnderLine = "<color:White>*</color>*<color:DarkGray>*</color>*<color:DarkGray>*</color><color:White>*</color><color:White>*</color>*<color:DarkGray>*</color>*<color:DarkGray>*</color><color:White>*</color>";

        public override void initializeGame()
        {
            displayWelcome();
            displayMainChoiceMenu();

        }

        public override void makePlay(int iPlayerIndex)
        {
            //make variable for player
            Player player = Board.access().getPlayer(iPlayerIndex);

           

            //Change the colour for the player
            Console.ForegroundColor = this.colors[iPlayerIndex];

            //if inactive skip turn
            if (player.isNotActive())
            {
                Console.WriteLine("\n{0} is inactive.\n", player.getName());
                //check players to continue
                //check that there are still two players to continue
                int activePlayerCount = 0;
                foreach (Player p in Board.access().getPlayers())
                {
                    //if player is active
                    if (!p.isNotActive())
                        //increment activePlayerCount
                        activePlayerCount++;
                }

                //if less than two active players display winner
                if (activePlayerCount < 2)
                {
                    this.printWinner();
                }
               
                return;
            }
            

            if (player.getIsInJail())
            {
                //let player try to roll out
                player.resetJailRoll();
                Console.WriteLine("{0}You are in jail", playerPrompt(iPlayerIndex));
                player.setPlayerActionCompleted(false);
                displayPlayerJailMenu(player);
                return;
            }

            //prompt player to make move
            Console.WriteLine("{0}Your turn. Press Enter to make move", playerPrompt(iPlayerIndex));
            getInput();
            //move player
            player.move();

            //Display making move
            
            Console.WriteLine("{0}Move for {1}:{2}", colorUnderLine, player.getName(), colorUnderLine);
            //Display rolling
            Console.WriteLine("{0}{1}\n", playerPrompt(iPlayerIndex), player.diceRollingToString());

            Property propertyLandedOn = Board.access().getProperty(player.getLocation());

          

            Console.WriteLine(propertyLandedOn.landOn(ref player));

            //reset color to players color
            Console.ForegroundColor = this.colors[iPlayerIndex];

            //Display player details
            Console.WriteLine("\n{0}{1}", playerPrompt(iPlayerIndex), player.BriefDetailsToString());

            
            //display player choice menu
            displayPlayerChoiceMenu(player);

            
        }

        public override bool endOfGame()
        {
            //display message
            
            Console.WriteLine("The game is now over. Please press enter to exit.");
            Console.ReadLine();
            //exit the program
            Environment.Exit(0);
            return true;
        }

        public override void printWinner()
        {
            Player winner = null;
            //get winner who is last active player
            foreach (Player p in Board.access().getPlayers())
            {
                //if player is active
                if (!p.isNotActive())
                    winner = p;
            }
             //display winner
            Console.WriteLine(ConsoleOveride.spacer);
            Console.WriteLine("\n\n<color:White>{0}</color> <color:Green>has won the game!!!!!</color>\n\n" , winner.getName());
            Console.WriteLine(ConsoleOveride.spacer);
            //end the game
            this.endOfGame();
        }
        /// <summary>
        /// Displays intro screen to game
        /// </summary>
        public void displayWelcome()
        {   //monoploy

            Console.WriteLine(ConsoleOveride.spacer);
            Console.WriteLine("\n\n");
            String newWord = ConsoleOveride.colorString("M o n o p o l y \tGame by ");
            newWord += "<color:White>Luke</color> <color:Gray>Hardiman</color>";
            String intro = String.Format("\tWelcome to {0}\n", newWord);
            Console.WriteLine(intro);
            Console.WriteLine("\n");
            Console.WriteLine(ConsoleOveride.spacer);
        }
        public void displayMainChoiceMenu()
        {
            int resp = 0;
            Console.WriteLine("\n\n<color:White>Main Menu</color>");
            Console.WriteLine(colorUnderLine);
            Console.WriteLine("Please make a selection:\n");
           
            Console.WriteLine("<color:White>1.</color><color:Yellow>Setup Monopoly Game</color>");
            Console.WriteLine("<color:White>2.</color><color:Green>Start New Game</color>");
            Console.WriteLine("<color:White>3.</color>Load Previous Game");
            Console.WriteLine("<color:White>4.</color><color:Red>Exit</color>");
            Console.WriteLine(colorUnderLine);
            Console.Write("(1-3)>");

            //read response
            resp = inputInteger();
            //if response is invalid redisplay menu
            if (resp == 0)
                this.displayMainChoiceMenu();

            //perform choice according to number input
            try
            {
                switch (resp)
                {
                    case 1:
                        this.setUpGame();
                        this.gameSetUp = true;
                        this.displayMainChoiceMenu();
                        break;
                    case 2:
                        if (this.gameSetUp)
                            this.playGame();
                        else
                        {
                            Console.WriteLine("The Game has not been set up yet.\n");
                            this.displayMainChoiceMenu();
                        }
                        break;
                    
                    case 3:
                        this.setUpProperties();
                        Board.access().loadGame();
                        
                        //bound bunkrumpt handlers 
                        ArrayList thePlayers = Board.access().getPlayers();

                        //no players loaded
                        if (thePlayers.Count == 0)
                        {
                            Console.WriteLine("No players found...");
                            this.displayMainChoiceMenu();
                        }
                            

                        foreach (Player thePlayer in thePlayers)
                        {
                           
                            //subscribe to events
                            thePlayer.playerBankrupt += playerBankruptHandler;
                            thePlayer.playerPassGo += playerPassGoHandler;

                          
                        }

                        
                        this.gameSetUp = true;

                        this.playGame();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        throw new ApplicationException("That option is not avaliable. Please try again.");
                }
            }
            catch(ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void setUpGame()
        {
            //setup properties
            this.setUpProperties();

            //add players
            this.setUpPlayers();
            
        }

        public void playGame()
        {
            //int lastPlayer = 0;
            while (Board.access().getPlayerCount() >= 2)
            {
                for (int i = 0; i < Board.access().getPlayerCount(); i++)
                {
                    
                    this.makePlay(i);
                    //Set this user as last player to roll
                    Board.access().setLastPlayer(i);
                }
            } 
        }

        public virtual int inputInteger() //0 is invalid input
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a number such as 1 or 2. Please try again.");
                return 0;
            }
        }

        public virtual decimal inputDecimal() //0 is invalid input
        {
            try
            {
                return decimal.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a decimal number such as 25.54 or 300. Please try again.");
                return 0;
            }
        }
        public virtual string getInput() //0 is invalid input
        {
            //here for future input checking
            return Console.ReadLine();
        }
        public decimal inputDecimal(string msg)
        {
            Console.WriteLine(msg);
             Console.Write(">");
            decimal amount = this.inputDecimal();

            //if response is invalid redisplay 
            if (amount == 0)
            {
                Console.WriteLine("That was not a valid amount. Please try again");
                this.inputDecimal(msg);
            }
            return amount;
        }

        public void setUpProperties()
        {

            //Create instances of property factories
            LuckFactory luckFactory = new LuckFactory();
            ResidentialFactory resFactory = new ResidentialFactory();
            TransportFactory transFactory = new TransportFactory();
            UtilityFactory utilFactory = new UtilityFactory();
            PropertyFactory genericFactory = new PropertyFactory();

            JailFactory jailFactory = new JailFactory();

            Board.access().addProperty(luckFactory.create("Go", false, 200));
         
            Board.access().addProperty(resFactory.create("Ohakune Carrot", 60, 6, 50, Game.PropertyGroup.GROUP_1));
            
           
            Board.access().addProperty(luckFactory.create("Community Chest", false, 0, Game.CardType.CommunityChest)); // not properly implemented just 50 benefit
            
            Board.access().addProperty(resFactory.create("Te Puke, Giant Kiwifruit", 60, 6, 50, Game.PropertyGroup.GROUP_1));
          
            Board.access().addProperty(luckFactory.create("Income Tax", true, 200));
           
             Board.access().addProperty(transFactory.create("Auckland International Airport",Game.PropertyGroup.TRANSPORT_GROUP));
         
             Board.access().addProperty(resFactory.create("Te Papa", 100, 10, 50, Game.PropertyGroup.GROUP_2));
           
            Board.access().addProperty(luckFactory.create("Chance", true, 50, Game.CardType.Chance)); // not properly implemented just 50 penalty
           
             Board.access().addProperty(resFactory.create("Waitangi Treaty Grounds", 100, 10, 50, Game.PropertyGroup.GROUP_2));
             Board.access().addProperty(resFactory.create("Larnach Castle", 120, 12, 50, Game.PropertyGroup.GROUP_2));

             Board.access().addProperty(jailFactory.create("Jail",false)); //not properly implemented just a property that does nothing

             Board.access().addProperty(resFactory.create("Cape Reinga Lighthouse", 140, 14, 100, Game.PropertyGroup.GROUP_3));
           
             //utilFactory
            Board.access().addProperty(utilFactory.create("Mobile Phone Company", Game.PropertyGroup.UTILITY_GROUP));
       
             Board.access().addProperty(resFactory.create("Lake Taupo", 140, 14, 100, Game.PropertyGroup.GROUP_3));
             Board.access().addProperty(resFactory.create("Queenstown Ski Fields", 160, 16, 100, Game.PropertyGroup.GROUP_3));
         
            Board.access().addProperty(transFactory.create("Dunedin Railway Station", Game.PropertyGroup.TRANSPORT_GROUP));
         
            Board.access().addProperty(resFactory.create("Fox Glacier", 180, 18, 100, Game.PropertyGroup.GROUP_4));
          
            Board.access().addProperty(luckFactory.create("Community Chest", false, 50, Game.CardType.CommunityChest)); // not properly implemented just 50 benefit
        
            Board.access().addProperty(resFactory.create("Milford Sound", 180, 18, 100, Game.PropertyGroup.GROUP_4));
            Board.access().addProperty(resFactory.create("Mt Cook", 200, 20, 100, Game.PropertyGroup.GROUP_4));

            Board.access().addProperty(genericFactory.create("Free Parking")); //not properly implemented just a property that does nothing

            Board.access().addProperty(resFactory.create("Ninety Mile Beach", 220, 22, 150, Game.PropertyGroup.GROUP_5));
          
            Board.access().addProperty(luckFactory.create("Chance", true, 50, Game.CardType.Chance)); // not properly implemented just 50 penalty
        
            Board.access().addProperty(resFactory.create("Golden Bay", 220, 22, 150, Game.PropertyGroup.GROUP_5));
            Board.access().addProperty(resFactory.create("Moeraki Boulders, Oamaru", 240, 24, 150, Game.PropertyGroup.GROUP_5));
        
            Board.access().addProperty(transFactory.create("Port Tauranga", Game.PropertyGroup.TRANSPORT_GROUP));
         
            Board.access().addProperty(resFactory.create("Waitomo Caves", 260, 26, 150, Game.PropertyGroup.GROUP_6));
            Board.access().addProperty(resFactory.create("Mt Maunganui", 260, 26, 150, Game.PropertyGroup.GROUP_6));
       
            Board.access().addProperty(utilFactory.create("Internet Service Provider", Game.PropertyGroup.UTILITY_GROUP));
            
            Board.access().addProperty(resFactory.create("Art Deco Buildings, Napier", 280, 28, 150, Game.PropertyGroup.GROUP_6));

            Board.access().addProperty(jailFactory.create("Go to Jail",true)); //not properly implemented just a property that does nothing
            
            Board.access().addProperty(resFactory.create("Cable Cars Wellington", 300, 30, 200, Game.PropertyGroup.GROUP_7));
            Board.access().addProperty(resFactory.create("Cathedral Square", 300, 30, 200, Game.PropertyGroup.GROUP_7));
          
            Board.access().addProperty(luckFactory.create("Community Chest", false, 50, Game.CardType.CommunityChest)); // not properly implemented just 50 benefit
            
            Board.access().addProperty(resFactory.create("The Square, Palmerston North", 320, 32, 200, Game.PropertyGroup.GROUP_7));
         
            Board.access().addProperty(transFactory.create("Picton Ferry", Game.PropertyGroup.TRANSPORT_GROUP));
        
            Board.access().addProperty(luckFactory.create("Chance", true, 50, Game.CardType.Chance)); // not properly implemented just 50 penalty
          
            Board.access().addProperty(resFactory.create("Pukekura Park, Festival of Lights", 350, 35, 200, Game.PropertyGroup.GROUP_8));
           
            Board.access().addProperty(luckFactory.create("Super Tax", true, 100));
          
            Board.access().addProperty(resFactory.create("Rangitoto", 400, 40, 200, Game.PropertyGroup.GROUP_8));
            
            Board.access().setSquares();
            Console.WriteLine("Properties have been setup");
            CommunityCards.access().shuffleCards();
            ChanceCards.access().shuffleCards();
           
            
        }

        public void setUpPlayers()
        {
            //Add players to the board
            Console.WriteLine("<color:White>How many players are playing?</color>");
            Console.Write("<color:Blue>(2-8)></color>");
            int playerCount = this.inputInteger();

            //if it is out of range then display msg and redo this method
            if ((playerCount < 2) || (playerCount > 8))
            {
                Console.WriteLine("That is an invalid amount. Please try again.");
                this.setUpPlayers();
            }

            //Ask for players names
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("<color:White>Please enter the name for Player</color> <color:Green>{0}</color>:", i + 1);
                Console.Write(">");
                string sPlayerName = getInput();
                Player player = new Player(sPlayerName);
                //subscribe to events
                player.playerBankrupt += playerBankruptHandler;
                player.playerPassGo += playerPassGoHandler;
                //add player 
                Board.access().addPlayer(player);
                Console.WriteLine("<color:Green>{0}</color> has been added to the game.", Board.access().getPlayer(i).getName());
            }

            Console.WriteLine("Players have been setup");
        }

        public string playerPrompt(int playerIndex)
        {
            return string.Format("{0}:\t", Board.access().getPlayer(playerIndex).getName());
        }

        public string playerPrompt(Player player)
        {
            return string.Format("{0}:\t", player.getName());
        }

        public virtual bool getInputYN(Player player, string sQuestion)
        {
            Console.WriteLine(playerPrompt(player) + sQuestion);
            Console.Write("y/n>");
            string resp = getInput().ToUpper();

            switch (resp)
            {
                case "Y":
                    return true;
                case "N":
                    return false;
                default:
                    Console.WriteLine("That answer cannot be understood. Please answer 'y' or 'n'.");
                    this.getInputYN(player, sQuestion);
                    return false;
            }
        }
        /// <summary>
        /// Displays the in jail menu to the user
        /// </summary>
        /// <param name="player">requires the player</param>
        public void displayPlayerJailMenu(Player player)
        {
            //preset that we have not completed an action yet
            

            //Only display this option if we are in jail
            if (player.getIsInJail() == false)
            {
                displayPlayerChoiceMenu(player);
                return;
            }
            int resp = 0;
            String theColor = "";
            Console.WriteLine("\n{0}Please make a selection:\n", playerPrompt(player));

           
                theColor = (player.playerMadeAction()) ? "Green" : "DarkYellow";

                Console.WriteLine("<color:White>1.</color> <color:" + theColor + ">Finish turn</color>");
            Console.WriteLine("<color:White>2.</color> <color:Yellow>View your details</color>");

            Console.WriteLine("<color:White>3.</color> Pay <color:Yellow>$</color><color:White>50.00</color> to get out of jail</color>");
           
            

            theColor = (player.rolledMaxDoublesOutOfJail() == false) ? "White" : "DarkYellow";

            Console.WriteLine("<color:White>4.</color> <color:" + theColor + ">Try roll doubles to get out of jail</color>");

            theColor = (player.hasGetOutJailCard()) ? "White" : "DarkYellow";

            Console.WriteLine("<color:White>5.</color> <color:" + theColor + ">Use get out jail card</color>");
          

            
            Console.Write("(1-5)>");
            //read response
            resp = inputInteger();
            //if response is invalid redisplay menu
            if (resp == 0)
                this.displayPlayerJailMenu(player);

            //perform choice according to number input
            switch (resp)
            {
                case 1:
                    if (player.playerMadeAction() == false)
                    {
                        Console.WriteLine("<color:Red>You need to complete a action before moving on!</color>");
                        this.displayPlayerJailMenu(player);
                    }
                    
                    break;
                case 2:
                    Console.WriteLine("==================================");
                    Console.WriteLine(player.FullDetailsToString());
                    Console.WriteLine("==================================");
                    this.displayPlayerJailMenu(player);
                    break;
                case 3:
                    player.setPlayerActionCompleted(true);
                    this.payJailFee(player);
                    this.displayPlayerJailMenu(player);
                    break;
                case 4:
                    player.setPlayerActionCompleted(true);
                    this.rollDoublesJail(player);
                    this.displayPlayerJailMenu(player);
                    break;
                case 5:
                    player.setPlayerActionCompleted(true);
                    this.useGetOutOfJail(player);
                    this.displayPlayerJailMenu(player);
                    break;
                default:
                    Console.WriteLine("That option is not avaliable. Please try again.");
                    this.displayPlayerJailMenu(player);
                    break;
            }
        }
        
        /// <summary>
        /// Displays the users menu based on what is availble to them
        /// </summary>
        /// <param name="player">the player</param>
        /// <param name="fullmenu">if we show the full menu to the user</param>
        public void displayPlayerChoiceMenu(Player player,bool fullmenu = false)
        {
            int resp = 0;
            
            //display menus based on users selection
            Property theBoardProp = Board.access().getProperty(player.getLocation());

            Console.WriteLine(ConsoleOveride.spacer);

            Console.WriteLine("\n{0}Please make a selection:\n", playerPrompt(player));

            //if player has rolled 3 in row go to jail
            if (player.rolled3inRow())
            {
                Console.WriteLine(ConsoleOveride.spacer);
                Console.WriteLine("{0} <color:Red>You rolled 3 in a row of doubles you have gone straight to jail</color>", player.getName());
                Console.WriteLine(ConsoleOveride.spacer);
                player.setIsInJail();
                
            }

            if (player.rolledDouble() && !player.rolled3inRow())
            {
                Console.WriteLine("<color:White>1.</color> <color:Green>Roll again</color>");
            }
            else
            {
                Console.WriteLine("<color:White>1.</color> <color:Green>Finish turn</color>");
            }
            

            Console.WriteLine("<color:White>2.</color> <color:Yellow>View your details & Full Player Options</color>");

            if (theBoardProp.availableForPurchase())
                Console.WriteLine("<color:White>3.</color> <color:Yellow>Purchase This Property</color>");

            if (theBoardProp.getOwner() == player || fullmenu == true)
            {
                //if reseidental show housing options
                if (theBoardProp is Residential || fullmenu == true) 
                {
                    //Only return current house if its residential
                    int currentHouse = (theBoardProp is Residential)?((Residential)theBoardProp).getHouseCount() : 0;

                    if (fullmenu == true)
                    {
                        Console.WriteLine("<color:White>4.</color> <color:Magenta>Buy houses for Property</color>");
                    }
                    else
                    {
                        if (((Residential)theBoardProp).hasHotel() == false)
                            Console.WriteLine("<color:White>4.</color> <color:Magenta>Buy {0} for Property</color>", (currentHouse >= 4) ? "Hotel" : "House");
                    }

                    //if we have houses show the sell house option
                    if (currentHouse >= 1 || fullmenu == true)
                        Console.WriteLine("<color:White>5.</color> <color:Magenta>Sell House for Property</color>");

                }


                Console.WriteLine("<color:White>6.</color> <color:Blue>Trade Property with Player</color>");

                if (theBoardProp.isMortgaged() == false || fullmenu == true)
                    Console.WriteLine("<color:White>7.</color> <color:DarkYellow>Mortagae Property</color>");
                
                
                if (theBoardProp.isMortgaged() || fullmenu == true)
                    Console.WriteLine("<color:White>8.</color> <color:DarkYellow>UN-Mortagae Property</color>");

                Console.WriteLine("<color:White>9.</color> Save Game");
                
                   
            }
           

           
            Console.Write("(1-8) or 9 to save game>");
            //read response
            resp = inputInteger();
            //if response is invalid redisplay menu
            if (resp == 0)
                this.displayPlayerChoiceMenu(player);

            //perform choice according to number input
                switch (resp)
                {
                    case 1:

                        //exit loop go back to game
                        if (player.rolledDouble() && !player.rolled3inRow())
                            this.makePlay(Board.access().getLastPlayerIndex());

                        
                        break;
                    case 2:
                        Console.WriteLine("==================================");
                        Console.WriteLine(player.FullDetailsToString());
                        Console.WriteLine("==================================");
                        this.displayPlayerChoiceMenu(player,true);
                        break;
                    case 3:
                        this.purchaseProperty(player);
                        this.displayPlayerChoiceMenu(player);
                        break;
                    case 4:
                        this.buyHouse(player);
                        this.displayPlayerChoiceMenu(player);
                        break;
                    case 5:
                        this.sellHouse(player);
                        this.displayPlayerChoiceMenu(player);
                        break;
                    case 6:
                        this.tradeProperty(player);
                        this.displayPlayerChoiceMenu(player);
                        break;
                    case 7:
                        this.mortgageProperty(player);
                        this.displayPlayerChoiceMenu(player);
                        break;
                    case 8:
                        this.UnmortgageProperty(player);
                        this.displayPlayerChoiceMenu(player);
                        break;
                    case 9:
                        Board.access().saveGame();
                        this.displayPlayerChoiceMenu(player);
                        break;
                    default:
                        Console.WriteLine("That option is not avaliable. Please try again.");
                        this.displayPlayerChoiceMenu(player);
                        break;
                }
        }
        /// <summary>
        /// Uses the get out of jail card for a user
        /// </summary>
        /// <param name="player">the player</param>
        public void useGetOutOfJail(Player player)
        {
            if (player.hasGetOutJailCard() == false)
            {
                player.setPlayerActionCompleted(false);
                Console.WriteLine("{0}Does not have get out of jail card...", this.playerPrompt(player));
                return;
            }

            player.useJailCard();
            Console.WriteLine("{0} Used jail card to get out...", this.playerPrompt(player) );
            return;
        }
        /// <summary>
        /// Lets a player try and roll doubles to get out of jail
        /// </summary>
        /// <param name="player">Player</param>
        public void rollDoublesJail(Player player)
        {
            //roll doubles to get out of jail

            if (player.rolledMaxDoublesOutOfJail())
            {
                player.setPlayerActionCompleted(false);
                Console.WriteLine("<color:Red>You have reached your max attempts for rolling out of jail pay $50</color>");
                return;
            }
            Console.WriteLine("{0}\n",  player.jailRollDice());

        }
        /// <summary>
        /// Lets a Player pay to get out of jail
        /// </summary>
        /// <param name="player">Player</param>
        public void payJailFee(Player player)
        {
            //check player has money to pay
            if (player.getBalance() < 50)
            {
                Console.WriteLine("{0}Does not have enough money to get out of jail .", this.playerPrompt(player));
                return;
            }

            //pay $50 to bank
            player.payJailFee();

            Console.WriteLine("{0}Paid to get out of jail .", this.playerPrompt(player));
            return;

        }
        /// <summary>
        /// Allows Player to purchase a Property
        /// </summary>
        /// <param name="player">Player</param>
        public void purchaseProperty(Player player)
        {
            //if property available give option to purchase else so not available
            if (Board.access().getProperty(player.getLocation()).availableForPurchase())
            {
                TradeableProperty propertyLocatedOn = (TradeableProperty)Board.access().getProperty(player.getLocation());
                bool respYN = getInputYN(player, string.Format("'{0}' is available to purchase for <color:Yellow>$</color>{1}. Are you sure you want to purchase it?", propertyLocatedOn.getName(), propertyLocatedOn.getPrice()));
                if (respYN)
                {
                    propertyLocatedOn.purchase(ref player);//purchase property
                    Console.WriteLine("{0}You have successfully purchased {1}.", playerPrompt(player), propertyLocatedOn.getName());
                }
            }
            else
            {
                Console.WriteLine("{0}{1} is not available for purchase.", playerPrompt(player), Board.access().getProperty(player.getLocation()).getName());
            }
        }
        /// <summary>
        /// Allows a player to buy a house on a property
        /// </summary>
        /// <param name="player">the player</param>
        public void buyHouse(Player player)
        {
            //create prompt
            string sPrompt = String.Format("{0}Please select a property to buy a house for:", this.playerPrompt(player));
            //create variable for propertyToBuy
            Residential propertyToBuyFor = null;
            if (player.getPropertiesOwnedFromBoard().Count == 0)
            {
                //write message
                Console.WriteLine("{0}You do not own any properties.", playerPrompt(player));
                //return from method
                return;
            }

            //get the property to buy house for
            Property property = this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPrompt);


            
            
            //check that it is a residential
            if (property.GetType() != (new Residential().GetType()))
            {
                Console.WriteLine("{0}A house can not be bought for {1} because it is not a Residential Property.", this.playerPrompt(player), property.getName());
                return;
               
            }
            
            propertyToBuyFor = (Residential)property;
            //if dont own any properties

            var playersProps = player.returnGroupProperties(propertyToBuyFor);

                       

            //check if he owns all property
            if (playersProps == null)
            {
                Console.WriteLine("{0}You do not own all of the property groups for {1} .", this.playerPrompt(player), propertyToBuyFor.getName());
                return;
            }


            //@todo return the property that is mortgaged
            if (this.anyGroupPropsMorgaged(playersProps))
            {
                 //we end here with all props of this group
                 Console.WriteLine("{0}You can not buy a house on this group till all properties are unmortgaed {1}", this.playerPrompt(player), propertyToBuyFor.getName());
                 return;
            }
           
            //Check if this prop is 1 next inline for a house
            //@todo return the property's that can be built on.
            if (!this.isNextHouseForBuild(playersProps, propertyToBuyFor))
            {
                //we end here with all props of this group
                Console.WriteLine("{0}You can not purchase a house for {1} property has more house than others in your group", this.playerPrompt(player), propertyToBuyFor.getName());
                return;
            }

            if (propertyToBuyFor.hasHotel())
            {
                Console.WriteLine("{0}The maximum house & hotel limit for {1} has been reached.", playerPrompt(player), propertyToBuyFor.getName());
                return;
            }
            //check that max houses has not been reached
            if (propertyToBuyFor.getHouseCount() >= Residential.getMaxHouses())
            {
                Console.WriteLine("{0}The maximum house limit for {1} of {2} houses has been reached.", playerPrompt(player), propertyToBuyFor.getName(), Residential.getMaxHouses());
                
                //confirm
                bool doBuyHouse = this.getInputYN(player, String.Format("You chose to buy a Hotel for {0}. Are you sure you want to purchase a Hotel for <color:Yellow>$</color>{1}?", propertyToBuyFor.getName(), (propertyToBuyFor.getHouseCost() * 2)));
                //if confirmed
                if (doBuyHouse)
                {
                    //buy the house
                    propertyToBuyFor.addHotel();
                    Console.WriteLine("{0}A new hotel for {1} has been bought successfully. You can not devolp anymore", playerPrompt(player), propertyToBuyFor.getName());
                }
            
            }



            else
            {
                
                
                //confirm
                bool doBuyHouse = this.getInputYN(player, String.Format("You chose to buy a house for {0}. Are you sure you want to purchase a house for <color:Yellow>$</color>{1}?", propertyToBuyFor.getName(), propertyToBuyFor.getHouseCost()));
                //if confirmed
                if (doBuyHouse)
                {
                    //buy the house
                    propertyToBuyFor.addHouse();
                    Console.WriteLine("{0}A new house for {1} has been bought successfully", playerPrompt(player), propertyToBuyFor.getName());
                }
            }
        }

        /// <summary>
        /// Allows a Player to sell a house on a propertie
        /// </summary>
        /// <param name="player">requires the player</param>
        public void sellHouse(Player player)
        {
            //create prompt
            string sPrompt = String.Format("{0}Please select a property to sell a house for:", this.playerPrompt(player));
            //create variable for propertyToBuy
            Residential propertyToSellFor = null;
            if (player.getPropertiesOwnedFromBoard().Count == 0)
            {
                //write message
                Console.WriteLine("{0}You do not own any properties.", playerPrompt(player));
                //return from method
                return;
            }

            //get the property to sell the house
            Property property = this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPrompt);


            //if dont own any properties

            //check that it is a residential
            if (property is Residential)
            {
                //cast to residential property
                propertyToSellFor = (Residential)property;
            }
            else //else display msg 
            {
                Console.WriteLine("{0}A house can not be sold for {1} because it is not a Residential Property.", this.playerPrompt(player), property.getName());
                return;
            }

            var playersProps = player.returnGroupProperties(propertyToSellFor);



            if (propertyToSellFor.hasHotel())
            {

                Console.WriteLine("{0}You have a hotel on {1} and the price for selling is half the purchased price.", playerPrompt(player), propertyToSellFor.getName());
                bool doBuyHouse = this.getInputYN(player, String.Format("Are you sure you want to sell this Hotel for <color:Yellow>$</color>{0}?", (propertyToSellFor.getHouseCost())));
                if (doBuyHouse)
                {
                    propertyToSellFor.sellHouse();
                }
                return;
            }

            //check that we have houses
            if (propertyToSellFor.getHouseCount() > 0)
            {
                

                //confirm
                bool doBuyHouse = this.getInputYN(player, String.Format("You currently have {0}. Are you sure you want to sell a House for <color:Yellow>$</color>{1}?", propertyToSellFor.getHouseCount(), propertyToSellFor.getHouseCost() / 2));
                //if confirmed
                if (doBuyHouse)
                {
                    propertyToSellFor.sellHouse();
                }

            }

            else
            {
                 Console.WriteLine("There are no houses on property to sell");

               
            }
            return;
        }
        /// <summary>
        /// Checks if any properties are morgaged for a group
        /// </summary>
        /// <param name="playersPropsGroup">list of users properties</param>
        /// <returns>true or false if any props are morgaged</returns>
        public bool anyGroupPropsMorgaged(ArrayList playersPropsGroup)
        {
            //lets do some prechecks
            for (int i = 0; i < playersPropsGroup.Count; i++)
            {

                //owned by this player
                Property thisProperty = (Property)playersPropsGroup[i];
                if (thisProperty.isMortgaged())
                {
                    return true;
                    
                }
                //

            }

            return false;
        }
        /// <summary>
        /// Checks if the property is next inline for a house to be placed
        /// </summary>
        /// <param name="playersPropsGroup">requires all properties in group</param>
        /// <param name="theProp">the property to check against</param>
        /// <returns>true or false if property can be placed</returns>
        public bool isNextHouseForBuild(ArrayList playersPropsGroup, Residential theProp)
        {
            int currentHouseOnProp = theProp.getHouseCount();

            //if we have no houses then there is no point checing the other props
            if (currentHouseOnProp <= 0)
                return true;

            //may as well make sure there is more group houses
            if (Board.access().countPropGroups(theProp.getGroup()) <= 1 || theProp.getGroup() == Game.PropertyGroup.NONE)
                return true;

            //lets do some prechecks
            for (int i = 0; i < playersPropsGroup.Count; i++)
            {

                //owned by this player
                Residential thisProperty = (Residential)playersPropsGroup[i];
                if (currentHouseOnProp <= thisProperty.getHouseCount() && theProp.getName() != thisProperty.getName())
                {
                    
                    return true;

                }
                //

            }

            return false;
        }
        /// <summary>
        /// Morgages property
        /// </summary>
        /// <param name="player">requires player</param>
        public void mortgageProperty(Player player)
        {
            //Morgage property.

            //create prompt
            string sPrompt = String.Format("{0}Please select a property to mortgage:", this.playerPrompt(player));

            //get the property to mortgage
            Property property = this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPrompt);
            //|| property.GetType() != (new Residential().GetType())
            if (property == null )
            {
                Console.WriteLine("You don't currently have any properties!");
                return;
            }

            if (property.isMortgaged())
            {
                Console.WriteLine("{0} is currently mortgaged can not morgage this property.", property.getName());
                return;
            }

            Decimal theMortgage = ((Property)property).mortgagePropertyPrice();
            ((Property)property).mortgageProperty();
               

            Console.WriteLine("{0} was recevied for morgage {1} .", theMortgage , property.getName());
        }

       

        /// <summary>
        /// Unmorgages property
        /// </summary>
        /// <param name="player">requires player</param>
        public void UnmortgageProperty(Player player)
        {
            //Morgage property.

            //create prompt
            string sPrompt = String.Format("{0}Please select a property to unmortgaged:", this.playerPrompt(player));

            //get the property to mortgage
            Property property = this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPrompt);

            if (property == null || property.GetType() != (new Residential().GetType()))
            {
                Console.WriteLine("You don't currently have any properties!");
                return;
            }

            if (!property.isMortgaged())
            {
                Console.WriteLine("{0} is not morgaged.", property.getName());
                return;
            }

            Decimal theMortgage = ((Residential)property).unMortgageProperty();

            Console.WriteLine("{0} was paid for unmorgaging {1} .", theMortgage, property.getName());
        }

        /// <summary>
        /// Allows trading of a property from 1 player to another
        /// </summary>
        /// <param name="player">Player</param>
        public void tradeProperty(Player player)
        {
            //create prompt
            string sPropPrompt = String.Format("{0}Please select a property to trade:", this.playerPrompt(player));
            //create prompt
            string sPlayerPrompt = String.Format("{0}Please select a player to trade with:", this.playerPrompt(player));

            //get the property to trade
            TradeableProperty propertyToTrade = (TradeableProperty)this.displayPropertyChooser(player.getPropertiesOwnedFromBoard(), sPropPrompt);

            //if dont own any properties
            if (propertyToTrade == null)
            {
                //write message
                Console.WriteLine("{0}You do not own any properties.", playerPrompt(player));
                //return from method
                return;
            }

            //get the player wishing to trade with
            Player playerToTradeWith = this.displayPlayerChooser(Board.access().getPlayers(), player, sPlayerPrompt);

            //get the amount wanted

            string inputAmtMsg = string.Format("{0}How much do you want for this property?", playerPrompt(player));

            decimal amountWanted = inputDecimal(inputAmtMsg);

            //confirm with playerToTradeWith
                //set console color
            ConsoleColor origColor = Console.ForegroundColor;
            int i = Board.access().getPlayers().IndexOf(playerToTradeWith);
            Console.ForegroundColor = this.colors[i];

            String theMessage = "";
            if (propertyToTrade.isMortgaged())
            {
                decimal morgagePrice  = propertyToTrade.unMortgagePropertyPrice();
                
               theMessage = string.Format("{0} wants to trade '{1}' with you for <color:Yellow>$</color>{2}. The property is currently morgaged you will have to pay the bank <color:Yellow>$</color>{3} to unmorgage as well \nDo you agree to pay <color:Yellow>$</color>{4} + morgage <color:Yellow>$</color>{5} for '{6}'"
                                           , player.getName()
                                           , propertyToTrade.getName()
                                           ,amountWanted
                                           , morgagePrice
                                           , amountWanted
                                           , morgagePrice
                                           , propertyToTrade.getName()
                                           );

            }else{
                theMessage = string.Format("{0} wants to trade '{1}' with you for <color:Yellow>$</color>{2}. Do you agree to pay <color:Yellow>$</color>{2} for '{1}'", player.getName(), propertyToTrade.getName(), amountWanted);
            }
                //get player response
            bool agreesToTrade = getInputYN(playerToTradeWith, theMessage);
            //resent console color
            Console.ForegroundColor = origColor;
            if (agreesToTrade)
            {
                Player playerFromBoard = Board.access().getPlayer(playerToTradeWith.getName());
                //player trades property

                player.tradeProperty(ref propertyToTrade, ref playerFromBoard, amountWanted);
                Console.WriteLine("{0} has been traded successfully. {0} is now owned by {1}", propertyToTrade.getName(), playerFromBoard.getName());
            }
            else
            {
                //display rejection message
                Console.WriteLine("{0}{1} does not agree to trade {2} for <color:Yellow>$</color>{3}", playerPrompt(player), playerToTradeWith.getName(), propertyToTrade.getName(), amountWanted);
            }     
        }

        public Property displayPropertyChooser(ArrayList properties, String sPrompt)
        {
            //if no properties return null
            if (properties.Count == 0)
                return null;

            Console.WriteLine(sPrompt);
            for (int i = 0; i < properties.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, properties[i].ToString());
            }
            //display prompt
            Console.Write("({0}-{1})>", 1, properties.Count);
            //get input
            int resp = this.inputInteger();

            //if outside of range
            if ((resp < 1) || (resp > properties.Count))
            {
                Console.WriteLine("That option is not avaliable. Please try again.");
                this.displayPropertyChooser(properties, sPrompt);
                return null;
            }
            else
            {
                //return the appropriate property
                return (Property) properties[resp - 1];
            }
        }

        public Player displayPlayerChooser(ArrayList players, Player playerToExclude, String sPrompt)
        {
            //if no players return null
            if (players.Count == 0)
                return null;
            Console.WriteLine(sPrompt);
            //Create a new arraylist to display
            ArrayList displayList = new ArrayList(players);

            //remove the player to exlude
            displayList.Remove(playerToExclude);

            //go through and display each
            for (int i = 0; i < displayList.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, displayList[i].ToString());
            }
            //display prompt
            Console.Write("({0}-{1})>", 1, displayList.Count);
            //get input
            int resp = this.inputInteger();

            //if outside of range
            if ((resp < 1) || (resp > displayList.Count))
            {
                Console.WriteLine("That option is not avaliable. Please try again.");
                this.displayPlayerChooser(players, playerToExclude, sPrompt);
                return null;
            }
            else
            {
                Player chosenPlayer = (Player) displayList[resp - 1];
                //find the player to return
                foreach (Player p in players)
                {
                    if(p.getName() == chosenPlayer.getName())
                        return p;
                }
                return null;
            }
        }

        public static void playerBankruptHandler(object obj, EventArgs args)
            {
                //cast to player
                Player p = (Player) obj;
                //display bankrupt msg
                Console.WriteLine(ConsoleOveride.spacer);
                Console.WriteLine("<color:White>{0}</color> <color:Red>IS BANKRUPT!</color>", p.getName().ToUpper());
                Console.WriteLine(ConsoleOveride.spacer);
               

            }

        public static void playerPassGoHandler(object obj, EventArgs args)
        {
            Player p = (Player)obj;
            Console.WriteLine("{0} has passed <color:Green>GO</color>.{0} has received <color:Yellow>$</color>200", p.getName());
        }


   }
}

