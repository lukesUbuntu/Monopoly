using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
    [TestFixture]
    public class _UtilityTest
    {
        [Test]
        public void testPayRent()
        
        {
            Board.access().resetBoard(true);
            Utility u = new Utility();

            Player p = new Player("John", 1500);
            Board.access().addPlayer(p);
            Board.access().addProperty(u);

            //move p so that utility rent can be calculated
            p.move();

            u.payRent(ref p);

            //get p last move
            int i = p.getLastMove();

            //check that p has played correct rent of 6 times last move
            decimal balance = 1500 - (6 * i);
            Assert.AreEqual(balance, p.getBalance());
        }
        [Test]
        public void testLandOn()
        {
            Board.access().resetBoard(true);
            Utility util = new Utility();

            //Create two players
            Player p1 = new Player("Bill");
            Player p2 = new Player("Fred", 1500);
            Board.access().addPlayer(p1);
            Board.access().addPlayer(p2);
            Board.access().addProperty(util);
            string msg;

            //test landon normally with no rent payable
            msg = util.landOn(ref p1);
            Console.WriteLine(msg);

            //set owner to p1
            util.setOwner(ref p1);

            //move p2 so that utility rent can be calculated
            p2.move();

            //p2 lands on util and should pay rent
            msg = util.landOn(ref p2);
            Console.WriteLine(msg);

            //check that correct rent  has been paid
            decimal balance = 1500 - (6 * p2.getLastMove());
            Assert.AreEqual(balance, p2.getBalance());
        }
    }
}
