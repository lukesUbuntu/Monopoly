using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Collections;

namespace MonopolyGame_9901623
{
    [TestFixture]
    class _TestSettings
    {
        [Test]
        public void test_constructor_xml()
        {
            Settingsxml xmlSettings = new Settingsxml();

            Assert.IsTrue(xmlSettings.bankowns.GetType() == typeof(Decimal));
        }


        [Test]
        public void test_constructor()
        {
            Settings settings = new Settings();

           Assert.IsNotNull(settings);
          
        }
        [Test]
        public void test_save_settings()
        {
            Settings settings = new Settings();
            decimal house_purchase = 100, rent = 50, house_cost = 60;
            //add player 1
            Player player1 = new Player("Player1", 1500);
            Board.access().addPlayer(player1);
            Residential theProp = new Residential("Supermarket", house_purchase, rent, house_cost);
            theProp.setOwner(ref player1);
            Board.access().addProperty(theProp);

            Player player2 = new Player("Player2", 1500);
            Board.access().addPlayer(player2);
            Residential theProp2 = new Residential("Supermarket", house_purchase, rent, house_cost);
            theProp2.setOwner(ref player2);
            Board.access().addProperty(theProp2);

            Banker.access().setBalance(5000);

            settings.save();

            //Assert.Pass();

        }
        [Test]
        public void test_settings_load()
        {
            Monopoly theGame = new Monopoly();
            //need to preload the props
            theGame.setUpProperties();
            Settings settings = new Settings();

            settings.load();
            //if loaded from above test we should have 2 players
            ArrayList players = Board.access().getPlayers();

            Assert.IsTrue(players.Count > 0);

            foreach (Player thePlayer in players)
            {

                //subscribe to events
                Assert.IsTrue(thePlayer.getBalance() > 50);
               

            }

            Assert.IsTrue(Banker.access().getBalance() > 100);

        }
    }
}
