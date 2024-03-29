﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MonopolyGame_9901623
{
    /// <summary>
    /// Overrides the main console.out this allows for capture of output and modify.
    /// </summary>
        public class ConsoleOveride : TextWriter
        {

            


           
            //private TextWriter originalOut = Console.Out;
            private TextWriter originalOut = Console.Out;

            private Regex regexColor = new Regex("<color:(.*?)>(.*?)</color>");
            /// <summary>
            /// Receives the first instance of the call for WriteLine
            /// </summary>
            /// <param name="consoleMessage">message</param>
            public override void WriteLine(string consoleMessage)
            {
                //Test if we have a color tag
                if (consoleMessage.Contains("</color>"))
                {
                    //we have a color tag append new line
                    publishColor(consoleMessage+"\n");
                }
                else
                {
                    //output to orginal console writeout
                    this.writeline(consoleMessage);
                }
                
            }
            /// <summary>
            /// Receives the first instance of the call for Write
            /// </summary>
            /// <param name="consoleMessage">message</param>
            public override void Write(string consoleMessage)
            {
                //sends back to our writeline
                this.WriteLine(consoleMessage);

            }
            public override Encoding Encoding
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            /// <summary>
            /// Formats a message and builds prop coloring for the string 
            /// </summary>
            /// <param name="consoleMessage"></param>
            public void publishColor(string consoleMessage)
            {
                
                //"1. <color:Red>Setup Monopoly Game</color> test after  <color:Blue>Setup Monopoly Game</color>sss"
                //regex the color code
                Regex regexpBeforeNew = new Regex("(.*?)<color:(.*)>", RegexOptions.IgnorePatternWhitespace);
                Regex regexSplit = new Regex("</color>", RegexOptions.IgnorePatternWhitespace);

                // Split on our regex
                string[] substrings = regexSplit.Split(consoleMessage);
                foreach (string match in substrings)
                {

                    if (match.Contains("<color:"))
                    {
                        //we have a color append the end color tag because i forgot my regex grouping :)
                        this.printColor(match + "</color>");
                    }
                    else
                    {
                        //this is not part of a color match so append to orginal write out
                        this.write(match);
                    }
                   
                    
                }

              
                
               
               
            }

            /// <summary>
            /// Splits the messages into colored strings and output the color between <color>tags</color>
            /// </summary>
            /// <param name="consoleMessage">themessage</param>
            public void printColor(String consoleMessage)
            {
                Regex regexpAfter = new Regex("</color>(.*)");
                Regex regexpBefore = new Regex("(.*)<color:(.*)>", RegexOptions.IgnorePatternWhitespace);
                //Regex regexMessage = new Regex("<color>(.*)</color>");
                consoleMessage = consoleMessage.Replace("\n", "\\n");

                var stringMessage = regexColor.Match(consoleMessage);
                var stringAfter = regexpAfter.Match(consoleMessage);
                var stringBefore = regexpBefore.Match(consoleMessage);

                //@todo when multiple regex for color we need to loop regexColor

                int lengthString = stringMessage.Length;

                if (stringMessage.Length > 1)
                {
                    //var theMessageFind = regexMessage.Match(consoleMessage);
                    string theColor = (stringMessage.Groups[1].ToString() == "0") ? "White" : stringMessage.Groups[1].ToString();
                    theColor = (stringMessage.Groups[1].ToString() == "Black") ? "White" : stringMessage.Groups[1].ToString();

                    //grab the colored message
                    String theMessage = stringMessage.Groups[2].ToString();
                    //Grab the text after the color code
                    String afterMessage = stringAfter.Groups[1].ToString();
                    //Grab the text before the color code
                    String beforeMessage = stringBefore.Groups[1].ToString();



                    //grab the default color scheme
                    ConsoleColor defaultColor = Console.ForegroundColor;
                  
                    beforeMessage = beforeMessage.Replace("\\n", "\n");

                    this.write(beforeMessage);
                    //@todo need to make sure color exists
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), theColor);

                    this.write(theMessage);
                    Console.ForegroundColor = defaultColor;
                    this.write(afterMessage);

                }
              

                //publish console
                //this.writeLine("->{0}", consoleMessage);
            }

            /// <summary>
            /// Random spacer for splitting
            /// </summary>
            public static String spacer = ConsoleOveride.colorString("*************************************************************");

            /// <summary>
            /// Takes a string and randoms color to it.
            /// </summary>
            /// <param name="theString">the message</param>
            /// <returns>randomize string full of color </returns>
            public static String colorString(String theString){

                Random randomGen = new Random();
                String newString = "";
                //String[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
                String[] colorNames = new String[8] { 
                    "Cyan", "Green", "Yellow", "Red", "Magenta", "Gray", "Blue", "DarkYellow"
                };

                int numColors = colorNames.Length;

                //split all the strings
                 foreach (char singleString in theString)
                 {
                     string colorName = colorNames[randomGen.Next(numColors)];
                     newString += String.Format("<color:{0}>{1}</color>", colorName, singleString.ToString());
                 }

                
                
                return newString;
            }

            /// <summary>
            /// writes out to the orginal console.Write
            /// </summary>
            /// <param name="theMessage">console message</param>
            protected virtual void write(String theMessage)
            {
                originalOut.Write(theMessage);
            }

            /// <summary>
            /// writes out to the orginal console.WriteLine
            /// </summary>
            /// <param name="theMessage">console message</param>
            protected virtual void writeline(String theMessage)
            {
                originalOut.WriteLine(theMessage);
            }
        }
    
}
