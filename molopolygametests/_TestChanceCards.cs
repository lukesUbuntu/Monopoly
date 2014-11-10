using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame_9901623
{

    /// <summary>
    /// Testing chance cards
    /// </summary>
    [TestFixture]
    class _TestChanceCards
    {
        //Setup players
        Player newPlayer1 = new Player("newPlayer 1", 1500);
        Player newPlayer2 = new Player("newPlayer 2", 1500);
        //extend our Chance Class
        testChanceCards ChanceCardsClass = new testChanceCards();

        public _TestChanceCards()
        {
                //add players to board
                Board.access().addPlayer(newPlayer1);
                Board.access().addPlayer(newPlayer2);
        }

        //Now test can take place
        [Test]
        public void test_Shuffle_and_draw()
        {
            //Draw cards till shuffle is invoked
            ChanceCards target = ChanceCards.access();
            target.shuffleCards();
            //grab the response
            String theResponse = target.draw_card(ref newPlayer1);
            //check the response is not empty
            Assert.IsNotNullOrEmpty(theResponse);
        }

        [Test]
        public void test_advance_to_go()
        {
            //Set players location away from go
            newPlayer1.setLocation(10);

            //target.your_birthday()

            ChanceCardsClass.setPlayer(ref newPlayer1);
            ChanceCardsClass.advance_to_go();


            StringAssert.StartsWith("Advance", ChanceCardsClass.returnResponse());
            Assert.True(newPlayer1.getLocation() == 0);
        }

        [Test]
        public void test_adance_to_random()
        {
          

            //We need to add some props
            Board.access().addProperty(new Residential("Residental1"));
            Board.access().addProperty(new Transport("Transport2"));
            Board.access().addProperty(new Utility("Utility3"));
            Board.access().addProperty(new Residential("Residental4"));
            Board.access().addProperty(new Transport("Transport5"));
            Board.access().addProperty(new Utility("Utility6"));
            Board.access().addProperty(new Residential("Residental7"));
            Board.access().addProperty(new Transport("Transport8"));
            Board.access().addProperty(new Utility("Utility9"));
            Board.access().addProperty(new Residential("Residental10"));

            ChanceCardsClass.setPlayer(ref newPlayer1);
            //get the advance_random card
            int current = 0;
            //lets draw card 3 times and make sure the location changes
            for (int i = 0; i <= 3; i++)
            {
                //get current location
                
                //draw card
                ChanceCardsClass.advance_random();
                
                //check we have moved
                Assert.True(newPlayer1.getLocation() != current);
                current = newPlayer1.getLocation();
            }
        }

        [Test]
        public void test_adance_to_utility()
        {
            //Set players location away from go
            newPlayer1.setLocation(5);

            //We need to add some props
            
            Board.access().addProperty(new Residential("Residental 1"));
            Board.access().addProperty(new Residential("Residental 2"));
            Board.access().addProperty(new Transport("Transport 1"));
            Board.access().addProperty(new Transport("Transport 2"));
            Board.access().addProperty(new Utility("Utility 2"));
            Board.access().addProperty(new Transport("Transport 3"));
            Board.access().addProperty(new Transport("Transport 4"));
            Board.access().addProperty(new Utility("Utility 3"));

            ChanceCardsClass.setPlayer(ref newPlayer1);
            //get the advance_to_utility card
            ChanceCardsClass.advance_to_utility();


            StringAssert.StartsWith(String.Format("{0} Advance token to nearest Utility",newPlayer1.getName()), ChanceCardsClass.returnResponse());
            //check we are not still on location 10
            Assert.True(newPlayer1.getLocation() != 10);
            //Check that we are on the nearest utility
            String playersLocation = Board.access().getProperty(newPlayer1.getLocation()).getRName();
            Assert.AreEqual("Utility 2", playersLocation);
        }

        [Test]
        public void test_adance_to_transport()
        {
            //Set players location away from go
            newPlayer1.setLocation(1);

            //We need to add some props

            Board.access().addProperty(new Residential("Residental 1"));
            Board.access().addProperty(new Residential("Residental 2"));
            Board.access().addProperty(new Transport("Transport 1"));
            Board.access().addProperty(new Transport("Transport 2"));
            Board.access().addProperty(new Utility("Utility 2"));
            Board.access().addProperty(new Transport("Transport 3"));
            Board.access().addProperty(new Transport("Transport 4"));
            Board.access().addProperty(new Utility("Utility 3"));

            ChanceCardsClass.setPlayer(ref newPlayer1);
            //get the advance_to_utility card
            ChanceCardsClass.advance_to_transport();


            StringAssert.StartsWith(String.Format("{0} Advance token to nearest Transport", newPlayer1.getName()), ChanceCardsClass.returnResponse());
            //check we are not still on location 10
            Assert.True(newPlayer1.getLocation() != 1);
            //Check that we are on the nearest utility
            String playersLocation = Board.access().getProperty(newPlayer1.getLocation()).getRName();
            Assert.AreEqual("Transport 1", playersLocation);
        }

        [Test]
        public void test_bank_pays_dividend()
        {
            //set players current balance + $20 
            decimal expectingBalance = newPlayer1.getBalance() + 50;

            //set player
            ChanceCardsClass.setPlayer(ref newPlayer1);

            //process card
            ChanceCardsClass.bank_pays_dividend();

            //get return message
            StringAssert.StartsWith("Bank pays you dividend of $50 ", ChanceCardsClass.returnResponse());
            //check that they match
            Assert.True(newPlayer1.getBalance() == expectingBalance);
        }

         [Test]
        public void testget_jail_free()
        {
            ChanceCardsClass.setPlayer(ref newPlayer1);
            //give the player jail card
            ChanceCardsClass.get_jail_free();


            //check response and has jail card
            StringAssert.StartsWith("Get out of jail free card received", ChanceCardsClass.returnResponse());
            Assert.True(newPlayer1.hasGetOutJailCard());
        }

         [Test]
         public void test_3_stepsBack()
         {
             Board.access().addProperty(new Residential("Residental 1"));
             Board.access().addProperty(new Residential("Residental 2"));
             Board.access().addProperty(new Transport("Transport 1"));
             Board.access().addProperty(new Transport("Transport 2"));
             Board.access().addProperty(new Utility("Utility 2"));
             Board.access().addProperty(new Transport("Transport 3"));
             Board.access().addProperty(new Transport("Transport 4"));
             Board.access().addProperty(new Utility("Utility 3"));

             //set players current location
             int current_location = 8;
             newPlayer1.setLocation(current_location);

             int expected_location = current_location - 3;

             ChanceCardsClass.setPlayer(ref newPlayer1);

             //give the player jail card
             ChanceCardsClass.go_back_3_spaces();


             //check response and has jail card
             StringAssert.StartsWith("Go back 3 spaces", ChanceCardsClass.returnResponse());
             Assert.AreEqual(expected_location,newPlayer1.getLocation());
         }

         [Test]
         public void testgo_to_jail()
         {

             //decimal expectedBalance = newPlayer1.getBalance() + 10;
             //set location as 0
             newPlayer1.setLocation(0);



             ChanceCardsClass.setPlayer(ref newPlayer1);

             //Testing
             ChanceCardsClass.go_to_jail();



             StringAssert.StartsWith("Go straight to jail", ChanceCardsClass.returnResponse());
             Assert.True(newPlayer1.getIsInJail());
             Assert.True(newPlayer1.getLocation() == 11);
         }

        [Test]
         public void test_street_repairs()
         {
             Residential theProp = new Residential("Residental 1");
             theProp.addHouses(4);
             theProp.setOwner(ref newPlayer1);
             //add property
             Board.access().addProperty(theProp);

             //get players current bank balance 4 props x 25
             decimal expected_balance = newPlayer1.getBalance() - (4 * 25);

                //set player
             ChanceCardsClass.setPlayer(ref newPlayer1);

                //draw card
             ChanceCardsClass.street_repairs();

             Assert.AreEqual(expected_balance, newPlayer1.getBalance());


         }

        [Test]
        public void test_pay_poor_tax()
        {
        
            //get players current bank balance - $20
            decimal expected_balance = newPlayer1.getBalance() - 20;

            //set player
            ChanceCardsClass.setPlayer(ref newPlayer1);

            //draw card
            ChanceCardsClass.pay_poor_tax();

            Assert.AreEqual(expected_balance, newPlayer1.getBalance());


        }
        [Test]
        public void test_elected_chair_person()
        {

            //there is only 1 player so balance should be -50
            decimal expected_balance = newPlayer1.getBalance() - 50;

            //set player
            ChanceCardsClass.setPlayer(ref newPlayer1);

            //draw card
            ChanceCardsClass.elected_chair_person();

            Assert.AreEqual(expected_balance, newPlayer1.getBalance());


        }

        [Test]
        public void test_building_loan_matures()
        {

            //receive $150
            decimal expected_balance = newPlayer1.getBalance() + 150;

            //set player
            ChanceCardsClass.setPlayer(ref newPlayer1);

            //draw card
            ChanceCardsClass.building_loan_matures();

            Assert.AreEqual(expected_balance, newPlayer1.getBalance());


        }
        [Test]
        public void test_cross_word_comp()
        {
            //receive $100
            decimal expected_balance = newPlayer1.getBalance() + 100;

            //set player
            ChanceCardsClass.setPlayer(ref newPlayer1);

            //draw card
            ChanceCardsClass.cross_word_comp();

            Assert.AreEqual(expected_balance, newPlayer1.getBalance());
        }

        [Test]
        public void testreturn_jail_card()
        {

            //remove card from deck
            ChanceCardsClass.remove_jail_card();

            ChanceCardsClass.setPlayer(ref newPlayer1);
            //give new player the jail free card
            ChanceCardsClass.get_jail_free();

            //player one uses get out of free card
            newPlayer1.useJailCard();

            ChanceCardsClass.return_jail_card();
            //check we have correct money
            Assert.True(newPlayer1.hasGetOutJailCard() == false);
        }
    }

    /// <summary>
    /// We require this to overide and set some settings in the orginal class without actually editing the source
    /// </summary>
    public class testChanceCards : ChanceCards
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
