using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{

     /// <summary>
    /// Implements the game interface
    /// </summary>
    public abstract class Game: GameInterface
    {
        private int playersCount;
    
        public abstract void initializeGame();
        public abstract void makePlay(int player);
        public abstract bool endOfGame();
        public abstract void printWinner();

        // A template method : 
        public void playOneGame(int playersCount)
        {
            this.playersCount = playersCount;
            initializeGame();
            int j = 0;
            while (!endOfGame())
            {
                makePlay(j);
                j = (j + 1) % playersCount;
            }
            printWinner();
        }

        public enum CardType
        {
            CommunityChest,
            Chance,
            None
        };
        public enum PropertyGroup
        {
            GROUP_1 = ConsoleColor.Gray,
            GROUP_2 = ConsoleColor.DarkCyan,
            GROUP_3 = ConsoleColor.Magenta,
            GROUP_4 = ConsoleColor.DarkYellow,
            GROUP_5 = ConsoleColor.Red,
            GROUP_6 = ConsoleColor.Yellow,
            GROUP_7 = ConsoleColor.Green,
            GROUP_8 = ConsoleColor.DarkBlue,
            UTILITY_GROUP = ConsoleColor.DarkMagenta,
            TRANSPORT_GROUP = ConsoleColor.DarkGreen,
            //no group spare
            NONE = ConsoleColor.White
        };
    }
}
