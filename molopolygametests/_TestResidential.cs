using System;
using System.Collections;
using System.Text;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
    [TestFixture]
    class _TestResidential
    {
        [Test]
        public void test_constructor()
        {
            //test owner
            Residential theProp = new Residential();
            Player player1 = new Player("Player1",1500);
            theProp.setOwner(ref player1);
            Assert.IsTrue(theProp.getOwner() == player1);
        }

        [Test]
        public void test_getRent()
        {
            //test owner
            decimal house_purchase = 100, rent = 50,house_cost = 60;
            //expected rent x 4 houses
            //rent is rental amount plus the rental amount for each house
            decimal expected_rent = rent + (rent * 4);

            Residential theProp = new Residential("test",house_purchase,rent,house_cost);
            //add 4 houses to property
            theProp.addHouses(4);
            //define owner
            Player player1 = new Player("Player1", 1500);
            theProp.setOwner(ref player1);
            Assert.IsTrue(theProp.getOwner() == player1);
           
            Assert.IsTrue(expected_rent == theProp.getRent());
           
        }

        [Test]
        public void test_addHouse()
        {
            //setup player 
            Player player1 = new Player("Player1", 1500);

            //define house costs & player expected
            decimal house_purchase = 100, rent = 50, house_cost = 60;
            decimal player1_expected_balance = player1.getBalance() - house_cost;

            Residential theProp = new Residential("Supermarket", house_purchase, rent, house_cost);
            theProp.setOwner(ref player1);

            //make sure no houses on prop
            Assert.IsTrue(theProp.getHouseCount() == 0);

            //add a house
            theProp.addHouse();

            Assert.IsTrue(theProp.getHouseCount() == 1);

            //check rent taken from player
            Assert.IsTrue(player1.getBalance() == player1_expected_balance);

        }
        [Test]
        public void test_addHotel()
        {
            //setup player 
            Player player1 = new Player("Player1", 1500);

            //define house costs & player expected
            decimal house_purchase = 100, rent = 50, house_cost = 60;
            decimal player1_expected_balance = player1.getBalance() - house_cost;

            Residential theProp = new Residential("Supermarket", house_purchase, rent, house_cost);
            theProp.setOwner(ref player1);

            //add a house
            theProp.addHotel();
            //make sure no hotels were added with out checking houses
            Assert.IsTrue(theProp.getHouseCount() == 0);
            Assert.IsFalse(theProp.hasHotel());
            //add 4 houses
            theProp.addHouses(4);
            theProp.addHotel();
            Assert.IsTrue(theProp.hasHotel());


        }

        [Test]
        public void test_SellHouse()
        {
            //setup player 
            Player player1 = new Player("Player1", 1500);

            //define house costs of house in half & player expected
            decimal house_purchase = 100, rent = 50, house_cost = 60;
            decimal player1_expected_balance = player1.getBalance() + (house_cost / 2);

            Residential theProp = new Residential("Supermarket", house_purchase, rent, house_cost);
            theProp.setOwner(ref player1);
            //add 1 house to prop with no charge
            theProp.addHouses(1);

            //sell the house
            theProp.sellHouse();

            //make sure no hotels were added with out checking houses
            Assert.IsTrue(theProp.getHouseCount() == 0);


            Assert.IsTrue(player1.getBalance() == player1_expected_balance);
            //lets add a hotel
            theProp.addHouses(5);
            theProp.sellHouse();
            Assert.IsTrue(theProp.getHouseCount() == 4);
        }

        [Test]
        public void test_mortgageProperty()
        {
            //setup player 
            Player player1 = new Player("Player1", 1500);
            Banker.access().setBalance(5000);

            //define house costs of house in half & player expected
            decimal house_purchase = 100, rent = 50, house_cost = 60;
           

            Residential theProp = new Residential("Supermarket", house_purchase, rent, house_cost);
            theProp.setOwner(ref player1);

            decimal player1_expected_balance = player1.getBalance() + theProp.mortgagePropertyPrice();
            
            theProp.mortgageProperty();
            Assert.AreEqual(house_cost, theProp.getHouseCost());
            //check balance
            Assert.IsTrue(player1.getBalance() == player1_expected_balance);
            
            //check prop is mortgaged
            Assert.IsTrue(theProp.isMortgaged());

            decimal player1_expeceted_balance_after = player1.getBalance() - theProp.unMortgageProperty();
            //now unmorgage
            
            Assert.IsTrue(player1.getBalance() == player1_expeceted_balance_after);
            Assert.IsFalse(theProp.isMortgaged());
        }


        [Test]
        public void test_mortgagevalue()
        {
            //setup player 
            Player player1 = new Player("Player1", 1500);
            Banker.access().setBalance(5000);

            //define house costs of house in half & player expected
            decimal house_purchase = 100, rent = 50, house_cost = 60;
            decimal unmortgagevalue = ((house_purchase / 2) * 10 / 100) + (house_purchase / 2);

            Residential theProp = new Residential("Supermarket", house_purchase, rent, house_cost);
            theProp.setOwner(ref player1);
            //check if they match
            Assert.AreEqual(unmortgagevalue, theProp.unMortgagePropertyPrice());
            
        }
    }
}
