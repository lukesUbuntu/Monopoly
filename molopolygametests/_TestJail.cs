using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
    [TestFixture]
    class _TestJail
    {
        [Test]
        public void test_construct()
        {
            Jail jailProp = new Jail("Jail",true);
            JailFactory jailFactory = new JailFactory();
            jailFactory.create("Jail", true);
            Assert.NotNull(jailProp);
        }
        [Test]
        public void test_construct2()
        {
            Jail jailProp = new Jail();
            JailFactory jailFactory = new JailFactory();
            jailFactory.create("Jail", true);
            Assert.NotNull(jailProp);
        }
        [Test]
        public void test_landOn_jail()
        {
            Jail jailProp = new Jail("Jail", true);
            Player thePlayer = new Player();
            thePlayer.setBalance(100);
            Board.access().addPlayer(thePlayer);
            String response = jailProp.landOn(ref thePlayer);
            //Check user has gone to jail
            Assert.IsTrue(thePlayer.getIsInJail());

            StringAssert.Contains("has gone to jail", response);
        }

        [Test]
        public void test_landOn_just_visting_jail()
        {
            Jail jailProp = new Jail("Jail", false);
            Player thePlayer = new Player();
            thePlayer.setBalance(100);
            Board.access().addPlayer(thePlayer);
            String response = jailProp.landOn(ref thePlayer);
            //Check user has gone to jail
            Assert.IsFalse(thePlayer.getIsInJail());

            StringAssert.Contains("just visting jail", response);
        }


    }
}
