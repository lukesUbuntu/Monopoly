using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{

    /// <summary>
    /// Interface for games
    /// </summary>
    
    interface GameInterface
    {
        void initializeGame();
        void makePlay(int player);
        bool endOfGame();
        void printWinner();
        void playOneGame(int playersCount);

    }
}
