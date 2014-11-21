
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame_9901623
{
    /// <summary>
    /// Testing comunity cards
    /// </summary>
    [TestFixture]
    class _TestCommunityCards 
    {
        
            //Setup players
            Player newPlayer1 = new Player("newPlayer 1", 1500);
            Player newPlayer2 = new Player("newPlayer 2", 1500);
            //extend our Community Class
            testCommunity CommunityClass = new testCommunity();
         
            public _TestCommunityCards(){
                //add players to board
                Board.access().addPlayer(newPlayer1);
                Board.access().addPlayer(newPlayer2);
            }

            [Test]
            public void testDeckShuffling()
            {
                //banker keeps going bankrupt on drawing 50 cards so we will increase there limi
                Banker.access().setBalance(500000);
                //Draw cards till shuffle is invoked
                CommunityCards target = CommunityCards.access();
                //invoke a manual shuffle
                target.shuffleCards();
                //lets draw 1 card from deck and caputre response as long as its not a jail free card received
                string first_draw = "jail free card";
                string received = "";
                do
                {
                   first_draw =  target.draw_card(ref newPlayer2);

                } while (first_draw.Contains("jail free card"));

                
                for (int card = 0; card < 50; card++)
                {
                     received = target.draw_card(ref newPlayer2);
                    //if we have received the same card means we have been shuffled
                    if (received.Contains(first_draw))
                    {
                        
                        break;
                    }
                }

                Assert.AreEqual(first_draw, received);
            }

            
            [Test]
            public void testadvance_to_go()
            {
                
                //Set players location away from go
                newPlayer1.setLocation(10);

               //target.your_birthday()

                CommunityClass.setPlayer(ref newPlayer1);
                CommunityClass.advance_to_go();


                StringAssert.StartsWith("Advance", CommunityClass.returnResponse());
                Assert.True(newPlayer1.getLocation() == 0);
            }

            [Test]
            public void testbank_error_in_favour()
            {
                //set players current balance + $75 for error in fac
                decimal expectingBalance = newPlayer1.getBalance() + 75;



                CommunityClass.setPlayer(ref newPlayer1);

                //process card
                CommunityClass.bank_error_in_favour();
                //get return message
                StringAssert.StartsWith("Bank error", CommunityClass.returnResponse());
                //check that they match
                Assert.True(newPlayer1.getBalance() == expectingBalance);
            }

            [Test]
            public void testbank_doctors_fees()
            {

                //set players current balance - $50 for doctors
                decimal expectingBalance = newPlayer1.getBalance() - 50;




                CommunityClass.setPlayer(ref newPlayer1);
                //test doctors fees
                CommunityClass.doctors_fees();

                //Check response and balance
                StringAssert.StartsWith(newPlayer1.getName() + " pay doctors fees", CommunityClass.returnResponse());
                Assert.True(newPlayer1.getBalance() == expectingBalance);
            }


            [Test]
            public void testget_jail_free()
            {
                CommunityClass.setPlayer(ref newPlayer1);
                //give the player jail card
                CommunityClass.get_jail_free();

               
                //check response and has jail card
                StringAssert.StartsWith("Get out of jail free card received", CommunityClass.returnResponse());
                Assert.True(newPlayer1.hasGetOutJailCard());
            }


         [Test]
            public void testgo_to_jail()
            {

                //decimal expectedBalance = newPlayer1.getBalance() + 10;
                //set location as 0
                newPlayer1.setLocation(0);



                CommunityClass.setPlayer(ref newPlayer1);

                //Testing
                CommunityClass.go_to_jail();



                StringAssert.StartsWith("Go straight to jail", CommunityClass.returnResponse());
                Assert.True(newPlayer1.getIsInJail());
                Assert.True(newPlayer1.getLocation() > 0);
            }
        
        

            [Test]
            public void testbeauty_contest()
            {

                //set expected balance 
                decimal expectedBalance = newPlayer1.getBalance() + 10;



                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.beauty_contest();



                StringAssert.StartsWith("You have won second prize", CommunityClass.returnResponse());
                //we should have additional $10
                Assert.True(newPlayer1.getBalance() == expectedBalance);
            }

            [Test]
            public void testinheritance()
            {

               //set expected balance aditonal $100
                decimal expectedBalance = newPlayer1.getBalance() + 100;




                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.inheritance();



                StringAssert.StartsWith("You inherit", CommunityClass.returnResponse());
                //we should have additional $100
                Assert.True(newPlayer1.getBalance() == expectedBalance);
            }

            [Test]
            public void testsale_of_stock()
            {

                //set expected balance aditonal $50
                decimal expectedBalance = newPlayer1.getBalance() + 50;




                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.sale_of_stock();



                StringAssert.StartsWith("From sale of stock", CommunityClass.returnResponse());
                //we should have additional $50
                Assert.True(newPlayer1.getBalance() == expectedBalance);
            }


            [Test]
            public void testholiday_fund()
            {


                //set expected balance aditonal $100
                decimal expectedBalance = newPlayer1.getBalance() + 100;




                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.holiday_fund();


                //check response message matches
                StringAssert.StartsWith("Holiday Fund matures", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == expectedBalance);
            }


            [Test]
            public void testyour_birthday()
            {
                //set expected balance 
                //new balances for players
                decimal newbalance_player1 = newPlayer1.getBalance() + (10 * Board.access().getPlayerCount()) - 10;
              



                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.your_birthday();

                StringAssert.StartsWith("Its your birthday you collected", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance_player1);
               
            }
            [Test]
            public void testreturn_jail_card()
            {

                //decimal expectedBalance = newPlayer1.getBalance() + 10;


                CommunityClass.remove_jail_card();
                CommunityClass.get_jail_free();

                //player should reseive $50.00
                newPlayer1.useJailCard();
                CommunityClass.return_jail_card();
                //check we have correct money
                Assert.True(newPlayer1.hasGetOutJailCard() == false);
            }

            [Test]
            public void testgrand_opera_night()
            {


                newPlayer1.setBalance(50);
                newPlayer2.setBalance(50);
                //player should reseive $50.00
                decimal newbalance_player1 = newPlayer1.getBalance() + (50 * Board.access().getPlayerCount()) - 50;
                



                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.grand_opera_night();
                
               
                //check response message matches
                StringAssert.StartsWith("Grand Opera Night collect", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == newbalance_player1);
               
            }
        


            [Test]
            public void testincome_tax_refund()
            {

                //playerShould receive $20
                decimal expectBalance = newPlayer1.getBalance() + 20;



                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.income_tax_refund();

                StringAssert.StartsWith("Income Tax refund", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == expectBalance);
            }

            [Test]
            public void testlife_insurance()
            {

                //set player expecting balance + $100
                decimal expectBalance = newPlayer1.getBalance() + 100;

               

                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.life_insurance();

 
                //check response message matches
                StringAssert.StartsWith("Life Insurance Matures", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == expectBalance);
            }

            [Test]
            public void testpay_hospital_fees()
            {

                //set player expecting balance - $100
                decimal expectBalance = newPlayer1.getBalance() - 100;

              

                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.pay_hospital_fees();

              
                //check response message matches
                StringAssert.StartsWith("Pay Hospital Fees", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == expectBalance);
            }

            [Test]
            public void testpay_school_fees()
            {

                //set player expecting balance - $50
                decimal expectBalance = newPlayer1.getBalance() - 50;



                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.pay_school_fees();

                //check response message matches
                StringAssert.StartsWith("Pay School Fees", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == expectBalance);
            }


            [Test]
            public void tesreceive_consultancy_fee()
            {

                //set player expecting balance - 25
                decimal expectBalance = newPlayer1.getBalance() + 25;



                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.receive_consultancy_fee();

                //check response message matches
                StringAssert.StartsWith("Receive $25 Consultancy", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == expectBalance);
            }


            [Test]
            public void tesreceive_street_repairs()
            {
                //set player expecting balance - $160; 4 x house @ $40
                decimal expectBalance = newPlayer1.getBalance() - 160;

                //add property with 4 houses
                //string sName, ref Trader owner, decimal dMortgageValue
                Residential theProp = new Residential("BMW House",500,50,50);
                theProp.setOwner(ref newPlayer1);
                theProp.addHouses(4);

                Board.access().addProperty(theProp);





                CommunityClass.setPlayer(ref newPlayer1);
                //Testing
                CommunityClass.street_repairs();

                
                //check response message matches
                StringAssert.StartsWith("You are assessed for street repairs", CommunityClass.returnResponse());
                //check we have correct money
                Assert.True(newPlayer1.getBalance() == expectBalance);
            }
            
    }
    /// <summary>
    /// We require this to overide and set some settings in the orginal class without actually editing the source
    /// </summary>
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
