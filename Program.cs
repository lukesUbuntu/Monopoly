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

        
        }

     
    }
}
