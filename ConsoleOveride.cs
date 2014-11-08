using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MonopolyGame_9901623
{

        class ConsoleOveride : TextWriter
        { 
            private TextWriter originalOut = Console.Out;
            private Regex regexColor = new Regex("<color:(.*?)>(.*?)</color>");
            public override void WriteLine(string consoleMessage)
            {
                //originalOut.WriteLine(value);
                //String ConsoleColor = extractColor(consoleMessage);
                if (consoleMessage.Contains("</color>"))
                {
                    //we have a message that requires some coloring
                    publishColor(consoleMessage+"\n");
                }
                else
                {
                    originalOut.WriteLine("->{0}", consoleMessage);
                }
                
            }

            public override Encoding Encoding
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            
            public void publishColor(string consoleMessage)
            {
                //"1. <color:Red>Setup Monopoly Game</color> test after  <color:Blue>Setup Monopoly Game</color>sss"
                
                Regex regexpBeforeNew = new Regex("(.*?)<color:(.*)>", RegexOptions.IgnorePatternWhitespace);
                Regex regexSplit = new Regex("</color>", RegexOptions.IgnorePatternWhitespace);

                     // Split on hyphens. 
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
                        originalOut.Write(match);
                    }
                   
                    
                }

              
                
               
               
            }

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

                    //PropertyGroups side = Game.PropertyGroup.GROUP_1;

                    //object val = Convert.ChangeType(side, side.GetTypeCode());
                    //originalOut.Write(val);

                    String theMessage = stringMessage.Groups[2].ToString();

                    String afterMessage = stringAfter.Groups[1].ToString();

                    String beforeMessage = stringBefore.Groups[1].ToString();



                    //grab the default color scheme
                    ConsoleColor defaultColor = Console.ForegroundColor;
                  
                    beforeMessage = beforeMessage.Replace("\\n", "\n");
                    originalOut.Write(beforeMessage);
                    //@todo need to make sure color exists
                    //Console.ForegroundColor = (ConsoleColor)theColor.ToString();
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), theColor);
                    originalOut.Write(theMessage);
                    Console.ForegroundColor = defaultColor;
                    originalOut.Write(afterMessage);

                }
              

                //publish console
                //originalOut.WriteLine("->{0}", consoleMessage);
            }


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
        }
    
}
