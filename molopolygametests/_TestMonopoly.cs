using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Collections;
using System.IO;

namespace MonopolyGame_9901623
{


    [TestFixture]
    class _TestMonopoly
    {
        Player player1, player2, player3;
        Residential theProp1, theProp2 , theProp3;
        Utility theProp4;
        MonopolyClass theGame ;
        consoleIntercept ourConsole = new consoleIntercept();
        public _TestMonopoly()
        {
            //we will get our test class to intercetp the console.writeline
            Console.SetOut(ourConsole);
            //reset board for this test

            resetBoard();
            //lets load props from actual game
            theGame  = new MonopolyClass();
            //theGame.setUpProperties();
        }
        public void resetBoard()
        {
            Board.access().resetBoard(true);

            //setup our players & props for testing
            player1 = new Player("Player1");
            player2 = new Player("Player2");
            player3 = new Player("Player3");

            theProp1 = new Residential("theProp1");
            theProp2 = new Residential("theProp1");
            theProp3 = new Residential("theProp1");
            theProp4 = new Utility();

            theProp1.setOwner(ref player1);
            theProp2.setOwner(ref player2);

            player1.setBalance(1500);
            player2.setBalance(1500);
            player3.setBalance(1500);

            Board.access().addProperty(theProp1);
            Board.access().addProperty(theProp2);
            Board.access().addProperty(theProp3);
            Board.access().addProperty(theProp4);
            Board.access().addPlayer(player1);
            Board.access().addPlayer(player2);
            Board.access().addPlayer(player3);
        }
        [Test]
        public void test_Monopoly_contruct(){
            Monopoly theGame = new Monopoly();
            Assert.IsNotNull(theGame);

            
        }

        [Test]
        public void test_UnmortgageProperty1()
        {
            //unmorge no props

           
            Board.access().getProperty(0).setOwner(ref player1);
            Board.access().getProperty(0).setIsMortgaged(false);
           // theProp1.setOwner(ref player1);
            //theProp1.mortgageProperty();

            theGame.resetUserinput();
            theGame.addKey("1");
            theGame.UnmortgageProperty(player1);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("is not morgaged", consoleResponse);
        }

        [Test]
        public void test_UnmortgageProperty2()
        {


            //set prop to be morgaged
            theProp1.setIsMortgaged(true);
            theProp1.setOwner(ref player1);
            MonopolyClass theGame = new MonopolyClass();
            theGame.resetUserinput();
            theGame.addKey("1");
            theGame.UnmortgageProperty(player1);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("was paid for unmorgaging", consoleResponse);
        }

        [Test]
        public void test_mortgageProperty()
        {
          
            theProp1.setIsMortgaged(false);


            //test mortgage with no props
           // MonopolyClass theGame = new MonopolyClass();
            theGame.resetUserinput();
            theGame.addKey("1");
            theGame.mortgageProperty(player1);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("was recevied for morgage", consoleResponse);
            
        }
        [Test]
        public void test_mortgagePropertyNoProps()
        {

            theProp3.setIsMortgaged(false);


            //test mortgage with no props
            //MonopolyClass theGame = new MonopolyClass();
            theProp3.setOwner(ref player1);
            theProp2.setOwner(ref player1);
            theProp1.setOwner(ref player1);
            theProp4.setOwner(ref player1);
            theGame.resetUserinput();
            theGame.addKey("1");
            theGame.mortgageProperty(player3);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("You don't currently have any properties", consoleResponse);

        }
        [Test]
        public void test_mortgagePropertyNotOwned()
        {

            theGame.resetUserinput();
            theGame.addKey("1");
            theGame.mortgageProperty(player3);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("You don't currently have any properties", consoleResponse);
        }
        [Test]
        public void test_buyHouse()
        {

            theProp1.setIsMortgaged(false);
           // MonopolyClass theGame = new MonopolyClass();

            theGame.resetUserinput();
            theGame.addKey("1");
            theGame.addKey("true");
            theGame.addKey("true");

            theProp1.addHouses(1);
            ourConsole.ClearConsole();
            theGame.buyHouse(player1);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("has been bought successfully", consoleResponse);
        }
        [Test]
        public void test_buyHouseNoProps()
        {

            theProp1.setIsMortgaged(false);
            //MonopolyClass theGame = new MonopolyClass();

            theGame.resetUserinput();
            theGame.addKey("1");
          

            ourConsole.ClearConsole();
            theProp3.setOwner(ref player2);

            theGame.buyHouse(player3);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("You do not own any properties", consoleResponse);
        }
        [Test]
        public void test_buyHouseUtility()
        {

            theProp1.setIsMortgaged(false);
            //MonopolyClass theGame = new MonopolyClass();
            theGame.resetUserinput();
            theGame.addKey("1");


            ourConsole.ClearConsole();
            theProp4.setOwner(ref player3);

            theGame.buyHouse(player3);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("because it is not a Residential Property", consoleResponse);
        }
        [Test]
        public void test_buyHotel()
        {

            theProp1.setIsMortgaged(false);
           // MonopolyClass theGame = new MonopolyClass();
            theProp1.addHouses(4);

            theGame.resetUserinput();
            theGame.addKey("1");
            theGame.addKey("true");
            theGame.addKey("true");

            ourConsole.ClearConsole();
            theGame.buyHouse(player1);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("has been bought successfully", consoleResponse);
            Assert.IsTrue(theProp1.hasHotel());
        }
        [Test]
        public void test_purchaseProperty()
        {
            //Lets fins a prop availble for purchase
            Banker theBanker = Banker.access();
            theProp1.setOwner(ref theBanker);
            ArrayList propsonboard = Board.access().getProperties();
            
            for (int propint = 0; propint < propsonboard.Count; propint++)
            {
                if (((Property)propsonboard[propint]).availableForPurchase()){
                    player1.setLocation(propint);
                    break;
                }
                     
            }
            theGame.resetUserinput();
            theGame.addKey("true");

            
            ourConsole.ClearConsole();
            theGame.purchaseProperty(player1);

            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("You have successfully purchased", consoleResponse);
           
           
        }

        [Test]
        public void test_displayWelcome()
        {


            //MonopolyClass theGame = new MonopolyClass();

            ourConsole.ClearConsole();
            theGame.displayWelcome();

            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("*", consoleResponse);

        }

        [Test]
        public void test_payJailFee()
        {


           // MonopolyClass theGame = new MonopolyClass();

            ourConsole.ClearConsole();
            //put player in jail
            player1.setIsInJail();
            theGame.payJailFee(player1);

            String consoleResponse1 = ourConsole.getOutput();
            StringAssert.Contains("Paid to get out of jail", consoleResponse1);

            ourConsole.ClearConsole();
            //put player in jail
            player1.setIsInJail();
            //set player1 no money
            player1.setBalance(40);
            theGame.payJailFee(player1);

            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("Does not have enough money to get out of jail", consoleResponse2);
        }
        [Test]
        public void test_useGetOutOfJail()
        {


           // MonopolyClass theGame = new MonopolyClass();

            ourConsole.ClearConsole();
            //put player in jail
            player1.setIsInJail();
            //try use jail card when we don't have 1
            theGame.useGetOutOfJail(player1);
            
            String consoleResponse1 = ourConsole.getOutput();
            StringAssert.Contains("Does not have get out of jail card", consoleResponse1);

            ourConsole.ClearConsole();

            //put player in jail
            player1.setIsInJail();
            //set player1 no money
            player1.giveGetOutJailCard();
            theGame.useGetOutOfJail(player1);

            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("Used jail card to get out", consoleResponse2);
        }

        [Test]
        public void test_RollDice()
        {


            //MonopolyClass theGame = new MonopolyClass();

            ourConsole.ClearConsole();
            //put player in jail
            player1.setIsInJail();
            theGame.rollDoublesJail(player1);

            String consoleResponse1 = ourConsole.getOutput();
            StringAssert.Contains("Rolling Dice", consoleResponse1);

            theGame.rollDoublesJail(player1);

            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("Already rolled", consoleResponse2);
           
        }

        [Test]
        public void test_sellHouseNoHouses()
        {


            //MonopolyClass theGame = new MonopolyClass();
           
            //put player in jail
            theProp1.setOwner(ref player1);
            ourConsole.ClearConsole();
            theGame.resetUserinput();

            theGame.addKey("1");
            theGame.addKey("true");
            theGame.sellHouse(player1);
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("There are no houses on property to sell", consoleResponse2);
        }

        [Test]
        public void test_sellHouse()
        {


           // MonopolyClass theGame = new MonopolyClass();

            //put player in jail
            theProp1.setOwner(ref player1);
            theProp1.addHouses(1);
            ourConsole.ClearConsole();
            theGame.resetUserinput();

            theGame.addKey("1");
            theGame.addKey("true");
            theGame.sellHouse(player1);
            //make sure house has been removed
            Assert.IsTrue(theProp1.getHouseCount() == 0);
        }
        [Test]
        public void test_sellHouseNoProps()
        {


            //MonopolyClass theGame = new MonopolyClass();

           //remove player 3 as owner
            theProp3.setOwner(ref player1);
           
            ourConsole.ClearConsole();

    
            theGame.sellHouse(player3);
           
            
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("You do not own any properties", consoleResponse2);
        }

        [Test]
        public void test_sellHotel()
        {


           // MonopolyClass theGame = new MonopolyClass();
            player1.setBalance(1500);
            //put player in jail
            theProp1.setOwner(ref player1);
            theProp1.addHouses(5);
            ourConsole.ClearConsole();
            theGame.resetUserinput();

            theGame.addKey("1");
            theGame.addKey("true");
           
            theGame.sellHouse(player1);
            //make sure house has been removed
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("You have a hotel on", consoleResponse2);
        }

        [Test]
        public void test_tradeProperty()
        {
            //need to reset our board props for this test
            resetBoard();
            
            //clear console
            ourConsole.ClearConsole();
            theGame.resetUserinput();

            //select property
            theGame.addKey("1");
            //select player
            theGame.addKey("1");
            //how much we want
            theGame.addKey("100");
            //confirm trade
            theGame.addKey("true");


            theGame.tradeProperty(player1);
            //make sure house has been removed
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("has been traded successfully", consoleResponse2);
            //check player2 now is owner
            Assert.AreSame(player2, theProp1.getOwner());
            
        }

        [Test]
        public void test_setup_players()
        {
            //wipe the board to fresh
            Board.access().resetBoard(true);
            theGame.resetUserinput();
            
            //select how many players
            theGame.addKey("2");
            //player1 name
            theGame.addKey("SAM");
            //player2 name
            theGame.addKey("JOHN");
            //confirm trade
            theGame.addKey("true");
            theGame.setUpGame();
            //theGame.setUpPlayers();
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("Players have been setup", consoleResponse2);
            
            Assert.IsTrue(Board.access().getPlayerCount() == 2);
            Assert.IsTrue(Board.access().getPlayer(0).getName() == "SAM");
            Assert.IsTrue(Board.access().getPlayer(1).getName() == "JOHN");

        }

        [Test]
        public void test_print_winner()
        {
            Player player1 = Board.access().getPlayer(0);
            player1.setBalance(-100);
            player1.isNotActive();
            player1.checkBankrupt();
            
            theGame.printWinner();
            //check console
            String consoleResponse2 = ourConsole.getOutput();
            //*** is the winning banner
            StringAssert.Contains("*", consoleResponse2);

        }

        [Test]
        public void displayPlayerJailMenu()
        {
            
            //this menu will only be allowed if the user is in jail
            player1.setIsInJail();
            //clear details
            theGame.resetUserinput();
            //set key to break out of display
            theGame.addKey("1");
            //show display to user
            player1.setPlayerActionCompleted(true);
            //theGame.displayPlayerJailMenu(player1);
            //check console
            String consoleResponse2 = ourConsole.getOutput();
            //if response is null then test completed back to the main menu as we exited safely else we are still stuck in jai;l
            Assert.Null(consoleResponse2);
        
            
        }

        [Test]
        public void test_playerPrompt()
        {
            resetBoard();
            //get all players on board
            ArrayList thePlayers = Board.access().getPlayers();
            for (int playindex = 0; playindex < thePlayers.Count; playindex++)
            
                //this should return the players name
                StringAssert.Contains(Board.access().getPlayer(playindex).getName(), theGame.playerPrompt(playindex));

        }
       
    }




    public class consoleIntercept : TextWriter
    {
        private string intercetped = null;
        public consoleIntercept()
        {

        }
        public override void WriteLine(string consoleMessage)
        {
            this.intercetped = consoleMessage;
        }
        public override void Write(string consoleMessage)
        {
            this.intercetped+= consoleMessage;
        }
        public void ClearConsole()
        {
            this.intercetped = "";
        }
        public override Encoding Encoding
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public string getOutput()
        {
            return this.intercetped;
        }


        
    }
    /// <summary>
    /// Overrides the main monoply so we can just force keyinput results.
    /// Not finished overrides 
    /// </summary>
    public class MonopolyClass : Monopoly
    {
        private ArrayList users_input_list = new ArrayList();
        private int users_input_count = 0;
        public MonopolyClass()
        {
            //we need to clear the props that are in access
           
        }
        public void addKey(string theKey)
        {
            users_input_list.Add(theKey);
        }
        public void resetUserinput()
        {
            users_input_count = 0;
            users_input_list = new ArrayList();
        }
        public override string getInput()
        {
            return users_input_list[users_input_count++].ToString();
        }
        //stop game causing test to crash
        public override bool endOfGame()
        {
            return true;
        }
        public override int inputInteger() //0 is invalid input
        {
            try
            {
                if (users_input_count < users_input_list.Count)
                {
                    return int.Parse(users_input_list[users_input_count++].ToString());

                }
                return 0;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a number such as 1 or 2. Please try again.");
                return 0;
            }
        }

        public override decimal inputDecimal() //0 is invalid input
        {
           
            try
            {
                return decimal.Parse(users_input_list[users_input_count++].ToString());
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a decimal number such as 25.54 or 300. Please try again.");
                return 0;
            }
        }

        public override bool getInputYN(Player player, string sQuestion) //0 is invalid input
        {
            
            try
            {
                return (users_input_list[users_input_count++].ToString() == "true") ? true : false;
            }
            catch (FormatException ex)
            {

                return false;
            }
        }



       
    }
}
