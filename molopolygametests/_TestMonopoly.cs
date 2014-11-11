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

        consoleIntercept ourConsole = new consoleIntercept();
        public _TestMonopoly()
        {
            //we will get our test class to intercetp the console.writeline
            Console.SetOut(ourConsole); 

            //setup our players & props for testing
            player1 = new Player("Player1");
            player2 = new Player("Player2");
            player3 = new Player("Player3");
            theProp1 = new Residential();
            theProp2 = new Residential();
            theProp3 = new Residential();
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
            
            MonopolyClass theGame = new MonopolyClass();
            Board.access().getProperty(1).setOwner(ref player1);
            

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
            MonopolyClass theGame = new MonopolyClass();

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
            MonopolyClass theGame = new MonopolyClass();
            theProp3.setOwner(ref player1);
            theProp2.setOwner(ref player1);
            theProp1.setOwner(ref player1);
            theProp4.setOwner(ref player1);

            theGame.addKey("1");
            theGame.mortgageProperty(player3);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("You don't currently have any properties", consoleResponse);

        }
        [Test]
        public void test_mortgagePropertyNotOwned()
        {
            //test for player who does not owned property

            MonopolyClass theGame = new MonopolyClass();

            theGame.addKey("1");
            theGame.mortgageProperty(player3);
            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("You don't currently have any properties", consoleResponse);
        }
        [Test]
        public void test_buyHouse()
        {

            theProp1.setIsMortgaged(false);
            MonopolyClass theGame = new MonopolyClass();

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
            MonopolyClass theGame = new MonopolyClass();

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
            MonopolyClass theGame = new MonopolyClass();

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
            MonopolyClass theGame = new MonopolyClass();
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

            
            MonopolyClass theGame = new MonopolyClass();

            player1.setLocation(2);

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


            MonopolyClass theGame = new MonopolyClass();

            ourConsole.ClearConsole();
            theGame.displayWelcome();

            String consoleResponse = ourConsole.getOutput();
            StringAssert.Contains("Welcome to", consoleResponse);

        }

        [Test]
        public void test_payJailFee()
        {


            MonopolyClass theGame = new MonopolyClass();

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


            MonopolyClass theGame = new MonopolyClass();

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


            MonopolyClass theGame = new MonopolyClass();

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


            MonopolyClass theGame = new MonopolyClass();
           
            //put player in jail
            theProp1.setOwner(ref player1);

            ourConsole.ClearConsole();

            theGame.addKey("2");
            theGame.addKey("true");
            theGame.sellHouse(player1);
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("There are no houses on property to sell", consoleResponse2);
        }

        [Test]
        public void test_sellHouse()
        {


            MonopolyClass theGame = new MonopolyClass();

            //put player in jail
            theProp1.setOwner(ref player1);
            theProp1.addHouses(1);
            ourConsole.ClearConsole();

            theGame.addKey("2");
            theGame.addKey("true");
            theGame.sellHouse(player1);
            //make sure house has been removed
            Assert.IsTrue(theProp1.getHouseCount() == 0);
        }
        [Test]
        public void test_sellHouseNoProps()
        {


            MonopolyClass theGame = new MonopolyClass();

           //remove player 3 as owner
            theProp3.setOwner(ref player1);
           
            ourConsole.ClearConsole();

            //try sell a house we don't have
            ourConsole.ClearConsole();
            theGame.sellHouse(player3);
           
            
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("You do not own any properties", consoleResponse2);
        }

        [Test]
        public void test_sellHotel()
        {


            MonopolyClass theGame = new MonopolyClass();
            player1.setBalance(1500);
            //put player in jail
            theProp1.setOwner(ref player1);
            theProp1.addHouses(5);
            ourConsole.ClearConsole();

            theGame.addKey("2");
            theGame.addKey("true");
            theGame.sellHouse(player1);
            //make sure house has been removed
            String consoleResponse2 = ourConsole.getOutput();
            StringAssert.Contains("You have a hotel on", consoleResponse2);
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
        public void addKey(string theKey)
        {
            users_input_list.Add(theKey);
        }
        public void resetUserinput()
        {
            users_input_count = 0;
            users_input_list = new ArrayList();
        }
        public override int inputInteger() //0 is invalid input
        {
            try
            {
                return int.Parse(users_input_list[users_input_count++].ToString());
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
