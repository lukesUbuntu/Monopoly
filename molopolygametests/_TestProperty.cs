using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
    [TestFixture]
    class _TestProperty
    {
        [Test]
        public void test_Construct()
        {
            Property theProp = new Property();
            StringAssert.Contains("Property", theProp.getRName());
            Assert.IsTrue(theProp.getOwner() == Banker.access());
        }

        [Test]
        public void test_Construct_pass()
        {
            //set trader
            Trader theTrader = new Trader();
            theTrader.setName("Trader");
            theTrader.setBalance(5000);
            
            //setup prop
            Property theProp = new Property("Garden", ref theTrader, 100);


            StringAssert.Contains("Garden", theProp.getRName());
            Assert.IsTrue(theProp.getOwner() == theTrader);
        }

        [Test]
        public void test_Construct_availble_purchase()
        {
            //set trader
            Trader theTrader = new Trader();
            theTrader.setName("Trader");
            theTrader.setBalance(5000);

            //setup prop
            Property theProp = new Property("Garden", ref theTrader, 100);
            

            StringAssert.Contains("Garden", theProp.getRName());
            Assert.IsFalse(theProp.availableForPurchase());
        }

        [Test]
        public void test_isMortgaged()
        {
            //set trader
            Trader theTrader = new Trader();
            theTrader.setName("Trader");
            theTrader.setBalance(5000);

            //setup prop
            Property theProp = new Property("Garden", ref theTrader, 100);


            StringAssert.Contains("Garden", theProp.getRName());
            Assert.IsFalse(theProp.isMortgaged());
        }

        [Test]
        public void test_mortgageProperty()
        {
            //set trader
            Trader theTrader = new Trader();
            theTrader.setName("Trader");
            theTrader.setBalance(5000);

            //setup prop
            Property theProp = new Property("Garden", ref theTrader, 100);
            theProp.mortgageProperty();

            StringAssert.Contains("Garden", theProp.getRName());
            Assert.IsTrue(theProp.isMortgaged());
        }

       
    }
}
