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
            //Use our console to override where the output goes
            ConsoleOverideClass consoleTest = new ConsoleOverideClass();

            Console.SetOut(consoleTest);
            //check if we can read the output
            String expected = "TEST";
            Console.Write(expected);

            //check
            String theOutput = consoleTest.getOutput();
            Assert.IsTrue(theOutput == expected);
          
        }

        [Test]
        public void test_ColorStripped()
        {
            //Use our console to override where the output goes
            ConsoleOverideClass consoleTest = new ConsoleOverideClass();

            Console.SetOut(consoleTest);

            //check if we can read the output
            String expected = "<color:Red>TEST</color>";

            Console.Write(expected);
            //grab the string
            String theOutput = consoleTest.getOutput();
            //the output should not be the same. it should have replaced the tags
            Assert.IsFalse(theOutput == expected);

            //the output should be "TEST\n"
            expected = "TEST\n";
            Assert.IsTrue(theOutput == expected);
        }

        [Test]
        public void test_ColorStrippedConsoleWriteLine()
        {
            //Use our console to override where the output goes
            ConsoleOverideClass consoleTest = new ConsoleOverideClass();

            Console.SetOut(consoleTest);

            //lets add multiple colors
            String expected = "<color:Red>TEST</color> <color:Yellow>ME</color> <color:White>OUT</color>";
            
            //switching to writeline
            Console.WriteLine(expected);

            //grab the string
            String theOutput = consoleTest.getOutput();

            //the output should not be the same. it should have replaced the tags
            Assert.IsFalse(theOutput == expected);

            //the output should be "TEST ME OUT"
            expected = "TEST ME OUT\n";
            Assert.IsTrue(theOutput == expected);
        }

       
    }



    public class ConsoleOverideClass : ConsoleOveride {

        private String getMessage = null;
        protected override void write(string theMessage)
        {
            this.getMessage += theMessage;
        }
        protected override void writeline(string theMessage)
        {
            this.getMessage = theMessage;
        }

        public string getOutput()
        {
            return this.getMessage;
        }
    }


}
