using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyGame_9901623
{
    //store all IEnums for projects
    public class IEnum
    {
       public enum Game
        {
            CommunityChest,
            Chance,
            None
        };
        /*
        Black = 0,
        DarkGreen = 2,
        DarkRed = 4,
        DarkMagenta = 5,
        DarkYellow = 6,
        Gray = 7,
        DarkGray = 8,
        Blue = 9,
        Green = 10,
        Cyan = 11,
        Red = 12,
        Magenta = 13,
        Yellow = 14,
        White = 15,
        */
       // ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Gray, ConsoleColor.Blue, ConsoleColor.DarkYellow
       public enum PropertyGroup
       {
           GROUP_1          = ConsoleColor.Gray,
           GROUP_2          = ConsoleColor.DarkCyan,
           GROUP_3          = ConsoleColor.Magenta,
           GROUP_4          = ConsoleColor.DarkYellow,
           GROUP_5          = ConsoleColor.Red,
           GROUP_6          = ConsoleColor.Yellow,
           GROUP_7          = ConsoleColor.Green,
           GROUP_8          = ConsoleColor.DarkBlue,
           UTILITY_GROUP    = ConsoleColor.DarkMagenta,
           TRANSPORT_GROUP  = ConsoleColor.DarkGreen,
           //no group spare
           NONE     = ConsoleColor.White
       };
       
    }
    /*
    public static class PropertyGroups
    {
        public const int GROUP_1 = 2;
        public const int GROUP_2 = 3;
        public const int GROUP_3 = 4;
        public const int GROUP_4 = 4;
        public const int NONE = 1;
    }
    */
}
