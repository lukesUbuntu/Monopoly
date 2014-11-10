using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MonopolyGame_9901623;
using System.IO;

namespace MonopolyGame_9901623
{
   
    [TestFixture]
    public class _TestConsoleOveride 
    {

        [Test]
        public void test_constructor()
        {
            //Use our console override
            Console.SetOut(new ConsoleOveride());

            //String theRead = Console.Write("<color:Red>test</color>")
            
            //override the console output so we can read it
            //StringReader readConsole = new StringReader();
            

            

        }


    }

    public class ConsoleOverideClass : ConsoleOveride {
        public String getTheMessage()
        {
            
        }
    }


}
