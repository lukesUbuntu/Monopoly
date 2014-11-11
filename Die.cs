using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{

    
    public class Die 
    {
        private static Random numGenerator = new Random();
        private int numberRolled;
        
        public int roll()
        {
            //numberRolled = 1;
            numberRolled = numGenerator.Next(1, 7);
            return numberRolled;
           
        }

        public int numberLastRolled()
        {
            return numberRolled;
        }
         
        public override string ToString()
        {
            return numberRolled.ToString();
        }
    }
}
