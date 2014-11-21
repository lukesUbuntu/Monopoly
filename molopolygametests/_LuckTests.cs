using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyGame_9901623;
using NUnit.Framework;
namespace MonopolyGame_9901623
{
    [TestFixture()]
    public class _LuckTests
    {
        [Test]
        public void LuckTests_landOn_chance()
        {
            Luck luckTest = new Luck("Rob", false, 10,Game.CardType.Chance);
            Player thePlayer = new Player();
            thePlayer.setBalance(100);
            Board.access().addPlayer(thePlayer);
            String response = luckTest.landOn(ref thePlayer);
            StringAssert.Contains("Drawing Chance Card", response);
            
        }

       [Test]
        public void LuckTests_landOn_Community()
        {
            Luck luckTest = new Luck("Rob", false, 10, Game.CardType.CommunityChest);
            Player thePlayer = new Player();
            thePlayer.setBalance(100);
            Board.access().addPlayer(thePlayer);
            String response = luckTest.landOn(ref thePlayer);
            StringAssert.Contains("Drawing Community Card", response);
        }



       [Test]
       public void LuckTests_landOn_Benefit()
       {
           //set luck not to draw card
           Luck luckTest = new Luck("Rob", true, 10, Game.CardType.None);
           Player thePlayer = new Player();
           thePlayer.setBalance(100);
           Board.access().addPlayer(thePlayer);
           String response = luckTest.landOn(ref thePlayer);
           StringAssert.Contains("has recieved", response);
       }

       [Test]
       public void LuckTests_landOn_penality()
       {
           //set luck not to draw card
           Luck luckTest = new Luck("Rob", false, 10, Game.CardType.None);
           Player thePlayer = new Player();
           thePlayer.setBalance(100);
           Board.access().addPlayer(thePlayer);
           String response = luckTest.landOn(ref thePlayer);
           StringAssert.Contains("has paid", response);
       }
    }
}
