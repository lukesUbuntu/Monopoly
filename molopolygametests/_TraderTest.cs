using System;
using System.Collections;
using System.Text;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
   
    /// <summary>
    /// Test for the Trader class
    /// </summary>
    

    [TestFixture]
    public class _TraderTest
    {
        

        [Test]
        public void nameAccessorModifierTest()
        {
            Trader t = new Trader();
            t.setName("Banker");
            Assert.AreEqual(t.getName(), "Banker");
          
        }

        [Test]
        public void balanceAccessorModifierTest()
        {
            Trader t = new Trader();
            t.setBalance(1050);
            Assert.AreEqual(t.getBalance(), 1050);

        }

        [Test]
        public void constructorTest()
        {          
            Trader t2 = new Trader("Player2", 1500);

            Assert.AreEqual(t2.getName(), "Player2");
            Assert.AreEqual(t2.getBalance(), 1500);
        }

        [Test]
        public void receiveTest()
        {
            Trader t = new Trader("Player1", 1500);

            t.receive(55);

            Assert.AreEqual(t.getBalance(), 1555);
        }

        [Test]
        public void payTest()
        {
            Trader t = new Trader("Player1", 1500);

            t.pay(111);

            Assert.AreEqual(t.getBalance(), 1500 - 111);
        }

        [Test]
        public void checkBankrupt()
        {
            Trader t = new Trader("Player1", 1500);

            try
            {
                t.checkBankrupt();//nothing should happen (no exception thrown)
                
            }
            catch (Exception ex)
            {
                Console.Write("Exception Thrown: " + ex.Message);
                Assert.Fail();
            }

        }

        [Test]
        public void checkBankruptZero()
        {
            Trader t = new Trader("Player1", 0);

            try
            {
                t.checkBankrupt();//exception should be thrown so should not run follwing line
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Console.Write("Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public void checkBankruptNegative()
        {
            Trader t = new Trader("Player1", -100);
            try
            {
                t.checkBankrupt();//exception should be thrown so should not run follwing line
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Console.Write("Exception Thrown: " + ex.Message);
            }

        }

        [Test]
        public void outputToString()
        {
            Trader t = new Trader();

            Console.Write(t);
        }


        [Test]
        public void propetyTest()
        {
            Trader t = new Trader();
            Property p = new Property("Queen Street");
            t.obtainProperty(ref p);
            Assert.Contains(p, t.getPropertiesOwned());

        }

        [Test]
        public void propety_receive()
        {
            Trader trader = new Trader();
            Property prop = new Property("Queen Street");
            decimal expected_balance = trader.getBalance() + 50;

            trader.obtainProperty(ref prop);
            
            //give the trander $50
            trader.receive(50);

            Assert.IsTrue(expected_balance == trader.getBalance());

        }
        [Test]
        public void propety_pay()
        {
            Trader trader = new Trader();
            Property prop = new Property("Queen Street");

            trader.setBalance(1500);
            decimal expected_balance = trader.getBalance() - 50;

            trader.obtainProperty(ref prop);

            //give the trander $50
            trader.pay(50);

            Assert.IsTrue(expected_balance == trader.getBalance());

        }
        [Test]
        
        public void test_check_bankRumpt() 
        {
           
            //Set player with bankrump
            Trader trader = new Trader("Player1", 0);

            try
            {
                trader.checkBankrupt();//nothing should happen (no exception thrown)
                Assert.Fail();

            }
            catch (ApplicationException ex)
            {
                Console.Write("Exception Thrown: " + ex.Message);
                Assert.IsTrue("Player1 is Bankrupt" == ex.Message.ToString());
                
            }

            
        }
        [Test]

        public void test_check_tradeProperty()
        {

            //Set players for trade
           
            Player player1 = new Player("Player1", 1500);
            Player player2 = new Player("Player2", 1500);

            TradeableProperty theTradeProp = new TradeableProperty();
            theTradeProp.setOwner(ref player1);
          
            
           
            decimal tradeAmount = 300;

            //ref TradeableProperty property, ref Player purchaser, decimal amount
            player1.tradeProperty(ref theTradeProp, ref player2, tradeAmount);

            //make sure player 2 is now the owner
           Assert.IsTrue(player2.getName() == theTradeProp.getOwner().getName());
           
        }

        public void test_check_tradePropertyMorgaged()
        {

            //Set players for trade

            Player player1 = new Player("Player1", 1500);
            Player player2 = new Player("Player2", 1500);

            
            

            TradeableProperty theTradeProp = new TradeableProperty();
            theTradeProp.setOwner(ref player1);
            theTradeProp.setIsMortgaged(true);


            decimal tradeAmount = 300;

            //ref TradeableProperty property, ref Player purchaser, decimal amount
            player1.tradeProperty(ref theTradeProp, ref player2, tradeAmount);

            //make sure player 2 is now the owner
            Assert.IsTrue(player2.getName() == theTradeProp.getOwner().getName());
            Assert.IsFalse(theTradeProp.isMortgaged());

        }

    }
}
