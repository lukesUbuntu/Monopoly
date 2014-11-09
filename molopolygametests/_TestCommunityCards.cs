
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame_9901623
{
    /// <summary>
    /// Testing comunity manager
    /// </summary>
    [TestFixture]
    class _TestCommunityCards 
    {
        
     
            [Test]
            public void testDeckShuffling()
            {
                //Draw cards till shuffle is invoked
                CommunityCards target = CommunityCards.access();
                target.shuffleCards();

                   
                
            }

            
            [Test]
            public void testadvance_to_go()
            {
                

                Player newPlayer1 = new Player("Sam",1500);
                //Set players location away from go
                newPlayer1.setLocation(10);

               //target.your_birthday()
                testCommunity theTest = new testCommunity();
                theTest.setPlayer(ref newPlayer1);
                theTest.advance_to_go();
                string response = theTest.returnResponse();

                StringAssert.StartsWith( "Advance", theTest.returnResponse());
                Assert.True(newPlayer1.getLocation() == 0);
            }

            [Test]
            public void testbank_error_in_favour()
            {
                decimal balance = 1500;
                //set player
                Player newPlayer1 = new Player("Sam", balance);
                Board.access().addPlayer(newPlayer1);

                decimal newbalance = balance + 75;
           
                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.bank_error_in_favour();

                string response = theTest.returnResponse();

                StringAssert.StartsWith("Bank error", theTest.returnResponse());
                Assert.True(newPlayer1.getBalance() == newbalance);
            }

            [Test]
            public void testbank_doctors_fees()
            {

                //set player
                Player newPlayer1 = new Player("Sam", 1500);
                //doctors fees
                int expecting = 1500 - 50;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.doctors_fees();

                string response = theTest.returnResponse();

                StringAssert.StartsWith("Sam pay doctors fees", theTest.returnResponse());
                Assert.True(newPlayer1.getBalance() == expecting);
            }


            [Test]
            public void testget_jail_free()
            {

                //set player
                Player newPlayer1 = new Player("Sam", 1500);
                //doctors fees
                

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.get_jail_free();

                string response = theTest.returnResponse();

                StringAssert.StartsWith("Get out of jail free card received", theTest.returnResponse());
                Assert.True(newPlayer1.hasGetOutJailCard());
            }


         [Test]
            public void testgo_to_jail()
            {

                //set player
                Player newPlayer1 = new Player("Sam", 1500);
                //doctors fees
                newPlayer1.setLocation(0);

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.go_to_jail();

                string response = theTest.returnResponse();

                StringAssert.StartsWith("Go straight to jail", theTest.returnResponse());
                Assert.True(newPlayer1.getIsInJail());
                Assert.True(newPlayer1.getLocation() == 11);
            }
        
        

            [Test]
            public void testbeauty_contest()
            {

                //set player
                Player newPlayer1 = new Player("Sam", 1500);


                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.beauty_contest();

                string response = theTest.returnResponse();

                StringAssert.StartsWith("You have won second prize", theTest.returnResponse());
                //we should have additional $10
                Assert.True(newPlayer1.getBalance() == 1510);
            }

            [Test]
            public void testinheritance()
            {

                //set player
                Player newPlayer1 = new Player("Sam", 1500);


                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.inheritance();

                string response = theTest.returnResponse();

                StringAssert.StartsWith("You inherit", theTest.returnResponse());
                //we should have additional $100
                Assert.True(newPlayer1.getBalance() == 1600);
            }

            [Test]
            public void testsale_of_stock()
            {

                //set player
                Player newPlayer1 = new Player("Sam", 1500);


                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.sale_of_stock();

                string response = theTest.returnResponse();

                StringAssert.StartsWith("From sale of stock", theTest.returnResponse());
                //we should have additional $50
                Assert.True(newPlayer1.getBalance() == 1550);
            }


            [Test]
            public void testholiday_fund()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                //player should reseive $50.00
                decimal newbalance = balance + 100;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.holiday_fund();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Holiday Fund matures", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance);
            }


            [Test]
            public void testyour_birthday()
            {

                //set players
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                Player newPlayer2 = new Player("Sam", balance);
                //add players to board
                Board.access().addPlayer(newPlayer1);
                Board.access().addPlayer(newPlayer2);
                //new balances for players
                decimal newbalance_player1 = balance + 10;
                decimal newbalance_player2 = balance - 10;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.your_birthday();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Its your birthday you collected", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance_player1);
                Assert.True(newPlayer2.getBalance() == newbalance_player2);
            }
            [Test]
            public void testreturn_jail_card()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                testCommunity theTest = new testCommunity();
                theTest.remove_jail_card();
                theTest.get_jail_free();

                //player should reseive $50.00
                newPlayer1.useJailCard();
                theTest.return_jail_card();
                //check we have correct money
                Assert.True(newPlayer1.hasGetOutJailCard() == false);
            }

            [Test]
            public void testgrand_opera_night()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Player 0", balance);
                Player newPlayer2 = new Player("Player 2", balance);
                //add players to board
                Board.access().addPlayer(newPlayer1);
                Board.access().addPlayer(newPlayer2);

                //player should reseive $50.00
                decimal newbalance_player1 = balance + 50;
                decimal newbalance_player2 = balance - 50;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.grand_opera_night();
                
                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Grand Opera Night collect", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance_player1);
                Assert.True(newPlayer2.getBalance() == newbalance_player2);
            }
        


            [Test]
            public void testincome_tax_refund()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                //player should reseive $20.00
                decimal newbalance = balance + 20;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.income_tax_refund();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Income Tax refund", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance);
            }

            [Test]
            public void testlife_insurance()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                //player should reseive $100.00
                decimal newbalance = balance + 100;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.life_insurance();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Life Insurance Matures", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance);
            }

            [Test]
            public void testpay_hospital_fees()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                //player should pay $100.00
                decimal newbalance = balance - 100;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.pay_hospital_fees();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Pay Hospital Fees", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance);
            }

            [Test]
            public void testpay_school_fees()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                //player should pay $50.00
                decimal newbalance = balance - 50;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.pay_school_fees();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Pay School Fees", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance);
            }


            [Test]
            public void tesreceive_consultancy_fee()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);
                //player should pay $25.00
                decimal newbalance = balance + 25;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.receive_consultancy_fee();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("Receive $25 Consultancy", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance);
            }


            [Test]
            public void tesreceive_street_repairs()
            {

                //set player
                decimal balance = 1500;
                Player newPlayer1 = new Player("Sam", balance);

                //add property with 4 houses
                //string sName, ref Trader owner, decimal dMortgageValue
                Residential theProp = new Residential("BMW House",500,50,50);
                theProp.setOwner(ref newPlayer1);
                theProp.addHouses(4);

                Board.access().addProperty(theProp);

                //player should pay 4 x $40 = 160
                decimal newbalance = balance - 160;

                testCommunity theTest = new testCommunity();

                theTest.setPlayer(ref newPlayer1);
                //Testing
                theTest.street_repairs();

                string response = theTest.returnResponse();
                //check response message matches
                StringAssert.StartsWith("You are assessed for street repairs", theTest.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance);
            }
            
    }
        public class testCommunity : CommunityCards
        {

            public void setPlayer(ref Player thePlayer)
            {
                this.the_player = thePlayer;
            }
            public string returnResponse()
            {
                return this.the_action_message;
            }
          
        }
    
}
