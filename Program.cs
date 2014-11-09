using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
     /// <summary>
    /// Main class for the program
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            //Set console to be overridden.
            Console.SetOut(new ConsoleOveride()); 
            
            Game game = new Monopoly();

            game.initializeGame();

            //Board.access().loadGame();

            //CommunityCards.access().shuffleCards();
            /*
            for (int i = 0; i < 2; i++)
            {


                Player player = new Player("player " + i);
                //subscribe to events
               
                //add player 
                Board.access().addPlayer(player);
                Console.WriteLine("{0} has been added to the game.", Board.access().getPlayer(i).getName());
            }

            Settings theSettings = new Settings();
            theSettings.save();
            */
            //String test = String.Format("\n{0} Drawing Community Card...\nCard Reads : {1}", player.getName(), CommunityCards.access().draw_card(ref player));
            //Console.WriteLine(test);
            //String cardDetails = CommunityManager.access().draw_card(ref thePlayer);

            //Console.WriteLine(cardDetails);

            //Console.WriteLine("1. <color:Red>Setup Game</color> test after  <color:Blue>Setup Monopoly Game</color>sss <color:Yellow>yellow</color>");
            //Console.ReadLine();
            //Settings theSettings = new Settings();

            //theSettings.save();


           
           //Console.WriteLine(newWord);

           //Console.ReadLine();
        }

     
    }
}
