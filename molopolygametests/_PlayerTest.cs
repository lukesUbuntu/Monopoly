using System;
using System.Collections;
using System.Text;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
    [TestFixture]
    class _PlayerTest
    {
        [Test]
        public void test_contruct()
        {
            Player thePlayer = new Player();
            Assert.IsNotNull(thePlayer);
            StringAssert.Contains(thePlayer.getName(), "Player");
            
        }

        [Test]
        public void test_jailRollDice()
        {
            Player thePlayer = new Player();

            //check first roll
            StringAssert.Contains("Rolling Dice", thePlayer.jailRollDice());
            //second roll should not allow

            // Rolled double out of jail
        }

        [Test]
        public void test_BriefDetailsToString()
        {
            Player thePlayer = new Player();
            thePlayer.setBalance(1500);
            Property theProp = new Property("Gardend");
            theProp.setOwner(ref thePlayer);
            Board.access().addProperty(theProp);

            //check first rolltheProp
            StringAssert.Contains("1500", thePlayer.BriefDetailsToString());
            //second roll should not allow

            // Rolled double out of jail
        }

        [Test]
        public void test_setLocation()
        {
            Player thePlayer = new Player();
             Property theProp = new Property("Gardend");
            theProp.setOwner(ref thePlayer);
            Board.access().addProperty(theProp);

            int boardS = Board.access().getSquares();
            //expected to receive 200 for passingo
            decimal expected = thePlayer.getBalance() + 200;
            //Set player location
            thePlayer.setLocation(++boardS);

            //check first roll
            Assert.IsTrue(expected == thePlayer.getBalance());
            //second roll should not allow

            // Rolled double out of jail
        }

        [Test]
        public void test_diceRollingToString()
        {
            Player thePlayer = new Player();

            Property theProp = new Property("Gardend");
            theProp.setOwner(ref thePlayer);
            Board.access().addProperty(theProp);

          
            

            //check first roll
            for (int diceroll = 0; diceroll < 5; diceroll++)
                StringAssert.Contains("Rolling Dice", thePlayer.diceRollingToString());
            //second roll should not allow

            // Rolled double out of jail
        }

        [Test]
        public void test_returnGroupProperties()
        {
            Player thePlayer = new Player();
            thePlayer.setBalance(1500); 

            //need to add several props with groups
            //lets add 2 transports and get the multiplier
            Transport transFactory1 = new Transport("Back Packers1", Game.PropertyGroup.TRANSPORT_GROUP);
            Transport transFactory2 = new Transport("Back Packers2", Game.PropertyGroup.TRANSPORT_GROUP);

            
            transFactory1.setOwner(ref thePlayer);
            transFactory2.setOwner(ref thePlayer);
            Board.access().addPlayer(thePlayer);
            Board.access().addProperty(transFactory1);
            Board.access().addProperty(transFactory2);

            ArrayList thePropList = thePlayer.returnGroupProperties(transFactory1);

            //check first rolltheProp
            Assert.IsNull(thePropList);
            //second roll should not allow

            // Rolled double out of jail
        }

        [Test]
        public void test_checkBankrupt()
        {
            //check bankrump we will set balance to 0 and return props to bank
            Player thePlayer = new Player();
            thePlayer.setBalance(0);
            Transport transFactory1 = new Transport("Back Packers1", Game.PropertyGroup.TRANSPORT_GROUP);
            transFactory1.setOwner(ref thePlayer);

            Board.access().addPlayer(thePlayer);
            Board.access().addProperty(transFactory1);
            thePlayer.checkBankrupt();

            Assert.IsTrue(transFactory1.getOwner() == Banker.access());
        }

        [Test]
        public void test_payJailFee()
        {
            //check bankrump we will set balance to 0 and return props to bank
            Player thePlayer = new Player();
            thePlayer.setBalance(100);
            thePlayer.setIsInJail();

            thePlayer.payJailFee();


            Assert.IsTrue(thePlayer.getBalance() == 50);
            Assert.IsFalse(thePlayer.getIsInJail());
        }
    }
}
