using System;
using System.Collections.Generic;
using System.Text;

namespace MolopolyGame
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
            //Console.WriteLine("1. <color:Red>Setup Game</color> test after  <color:Blue>Setup Monopoly Game</color>sss <color:Yellow>yellow</color>");
            //Console.ReadLine();
            //Settings theSettings = new Settings();
            //theSettings.save();
            //Console.ReadLine();
        }

     
    }
}
