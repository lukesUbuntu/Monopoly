using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyGame_9901623;
using NUnit.Framework;

namespace MonopolyGame_9901623
{
    [TestFixture]
    class _TestTransport
    {
        [Test]
        public void test_TransportFactory_mainConstructor()
        {
            Transport transFactory = new Transport();

            Assert.AreEqual("Railway Station", transFactory.getRName().ToString());
        }

        [Test]
        public void test_getRentOwner()
        {
            //set new player & transport prop
            Player newPlayer1 = new Player("newPlayer 1", 1500);
            Transport transFactory = new Transport("Back Packers");
            Board.access().addPlayer(newPlayer1);
            transFactory.setOwner(ref newPlayer1);

            //add prop
            Board.access().addProperty(transFactory);

            //should be no rent just say we landed on prop
            StringAssert.StartsWith(String.Format("{0} landed on {1}", newPlayer1.getName(), transFactory.getName()), transFactory.landOn(ref newPlayer1).ToString());

            //Assert.AreEqual(, transFactory.getRName().ToString());
        }


        [Test]
        public void test_get_rent_other_player()
        {
            //set new player & transport prop
            Player newPlayer1 = new Player("newPlayer 1", 1500);
            Player newPlayer2 = new Player("newPlayer 1", 1500);


            Transport transFactory = new Transport("Back Packers");

            Board.access().addPlayer(newPlayer1);
            Board.access().addPlayer(newPlayer2);
            transFactory.setOwner(ref newPlayer1);

            //add prop
            Board.access().addProperty(transFactory);
            //player 2 lands on transport

            //should be no rent just say we landed on prop
            StringAssert.StartsWith(String.Format("{0} owns 1, rent is $25", newPlayer1.getName()), transFactory.landOn(ref newPlayer2).ToString());

            //Assert.AreEqual(, transFactory.getRName().ToString());
        }

        [Test]
        public void test_get_rent_other_multiplier()
        {
            //set new player & transport prop
            Player newPlayer1 = new Player("newPlayer 1", 1500);
            Player newPlayer2 = new Player("newPlayer 1", 1500);

            //lets add 2 transports and get the multiplier
            Transport transFactory1 = new Transport("Back Packers",Game.PropertyGroup.TRANSPORT_GROUP);
            Transport transFactory2 = new Transport("Back Packers", Game.PropertyGroup.TRANSPORT_GROUP);
            Transport transFactory3 = new Transport("Back Packers", Game.PropertyGroup.TRANSPORT_GROUP);

            Board.access().addPlayer(newPlayer1);
            Board.access().addPlayer(newPlayer2);
            transFactory1.setOwner(ref newPlayer1);
            transFactory2.setOwner(ref newPlayer1);
            transFactory3.setOwner(ref newPlayer1);
            //add prop
            Board.access().addProperty(transFactory1);
            Board.access().addProperty(transFactory2);
            Board.access().addProperty(transFactory3);
            //player 2 lands on transport
           
            //should be no rent just say we landed on prop
            StringAssert.StartsWith(String.Format("{0} owns 3, rent is $100", newPlayer1.getName()), transFactory1.landOn(ref newPlayer2).ToString());

            //Assert.AreEqual(, transFactory.getRName().ToString());
        }
    }
}
