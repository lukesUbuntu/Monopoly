using System;
using System.Collections;
using System.Text;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
    [TestFixture]

    class _TestTradeableProperty
    {
        [Test]
        public void test_construct()
        {
            TradeableProperty theProp = new TradeableProperty();
            //default is 200
            Assert.AreEqual(200, theProp.getPrice());
        }
        
        [Test]
        public void test_pay_rent()
        {
            Player newPlayer1 = new Player("player1", 1500);
            Player newPlayer2 = new Player("player2", 1500);

            decimal expected_player1 = newPlayer1.getBalance() + 100;
            decimal expected_player2 = newPlayer2.getBalance() - 100;


            TradeableProperty theTradeProp = new TradeableProperty();
            theTradeProp.setOwner(ref newPlayer1);
            theTradeProp.payRent(ref newPlayer2);

            Assert.AreEqual(expected_player1, newPlayer1.getBalance());
            Assert.AreEqual(expected_player2, newPlayer2.getBalance());
        }

        [Test]
        public void test_bank_owns_purchase()
        {
            Player newPlayer1 = new Player("player1", 1500);
            Player newPlayer2 = new Player("playe2", 1500);

            TradeableProperty theTradeProp = new TradeableProperty();
            theTradeProp.setOwner(ref newPlayer1);
            //this will throw error as prop should not be availble
            try {
                theTradeProp.purchase(ref newPlayer2);
               
            }
            catch (ApplicationException e)
            {
                StringAssert.Contains("The property is not available from purchase", e.ToString());
            }
            
            
        }

        [Test]
        public void test_player_purchase()
        {

            Player newPlayer1 = new Player("player1", 1500);
            

            TradeableProperty theTradeProp = new TradeableProperty();
           

            //this will throw error as prop should not be availble
            try
            {
                theTradeProp.purchase(ref newPlayer1);

            }
            catch (ApplicationException e)
            {
                Assert.Fail();
            }

            Assert.IsTrue(theTradeProp.getOwner() == newPlayer1);


        }
    }
}
