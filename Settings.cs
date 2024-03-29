﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MonopolyGame_9901623
{
    /// <summary>
    /// Loads game saved data and properties
    /// </summary>
    public class Settings
    {
        private String playerGameFile = @"playergamesave.xml";


        String strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

       


        /// <summary>
        /// Load settings from file
        /// </summary>
        public void load()
        {
            //load up our xml
            Settingsxml gameData;

            String file = new Uri(Path.Combine(strAppDir, playerGameFile)).LocalPath;
            if (File.Exists(file))
            {
                XmlSerializer objXMLSerializer = new XmlSerializer(typeof(Settingsxml));
                FileStream objFS = new FileStream(file, FileMode.Open);
                gameData = (Settingsxml)objXMLSerializer.Deserialize(objFS);
                objFS.Close();
                //@todo error checking on file

                //set bank has
                Console.WriteLine("Bank Has received : $" + gameData.bankowns);
                Banker.access().setBalance(gameData.bankowns);



                foreach (PlayerSettings theplayer in gameData.Players)
                {
                    //create new player
                    Player newPlayer = new Player(theplayer.playerName);

                    //set new player details
                    newPlayer.setLocation(theplayer.playersLocation);
                    newPlayer.setBalance(theplayer.playersAccount);


                    //update jail card
                    if (theplayer.getOutOfJailCard)
                    {
                        newPlayer.giveGetOutJailCard();
                        CommunityCards.access().remove_jail_card();
                    }


                    //update property data
                    foreach (propdetails theProp in theplayer.PropertiesOwned)
                    {
                        updateProp(theProp, ref newPlayer);
                    }
                    // theplayer.PropertiesOwned

                    if (theplayer.inJail)
                    {
                        Console.WriteLine("{0} is set in Jail.", newPlayer.getName());
                        newPlayer.setIsInJail();
                    }
                   
                    //Board.access().getProperty()

                    Board.access().addPlayer(newPlayer);
                    Console.WriteLine(newPlayer.FullDetailsToString());
                    Console.WriteLine("{0} has been added to the game.", newPlayer.getName());
                    Console.WriteLine(ConsoleOveride.spacer);
                }

            }
            else
            {
                Console.WriteLine("No game data file found.. Start new game then save");
            }
           

 
  
        }
        private void updateProp(propdetails theProp, ref Player theplayer)
        {
            //Search the board for props
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                Property boardProp = Board.access().getProperty(i);

                if (boardProp.getRName() == theProp.sName)
                {
                    boardProp.setOwner(ref theplayer);
                    boardProp.setIsMortgaged(theProp.isMorgaged);
                    //if prop is residental add houses
                    if (boardProp is Residential)
                    {
                        ((Residential)boardProp).addHouses(theProp.houses);
                    }
                }
            }
        }
        /// <summary>
        /// save settings
        /// </summary>
        public void save()
        {
            String file = new Uri(Path.Combine(strAppDir, playerGameFile)).LocalPath;
           
            //set the game data
            Settingsxml gameData = new Settingsxml();

            //Store our player data
            List<PlayerSettings> playersData = new List<PlayerSettings>();


            //store props owned
            List<propdetails> thePropsOwned = null;
            //get all players
            ArrayList thePlayers = Board.access().getPlayers();

            //get all props
            ArrayList theProperties = Board.access().getProperties();

            foreach (Player theplayer in thePlayers)
            {
                //create a new list
                thePropsOwned = new List<propdetails>();

                //loop all players props
                foreach(Property theProp in theProperties)
                {
                    if (theProp.getOwner() == theplayer)
                    {
                        //propdetails theClass = new propdetails(0, false);
                        
                        if (theProp is Residential)
                        {
                            thePropsOwned.Add(new propdetails(theProp.getRName(), theProp.isMortgaged(), ((Residential)theProp).getHouseCount()));
                        }
                        else
                        {
                            
                            thePropsOwned.Add(new propdetails(theProp.getRName(), theProp.isMortgaged()));
                        }
                        
                    }

                }




                playersData.Add(new PlayerSettings(theplayer.getName(), theplayer.getBalance(), theplayer.getLocation(), theplayer.hasGetOutJailCard(), theplayer.getIsInJail(), thePropsOwned));
            }

         

            gameData.Players = playersData;
            gameData.bankowns = Banker.access().getBalance();

            XmlSerializer objXMLSerializer = new XmlSerializer(typeof(Settingsxml));
            FileStream objFS = new FileStream(file, FileMode.Create);
            objXMLSerializer.Serialize(objFS, gameData);
            objFS.Close();

           Console.WriteLine("Game Data Saved");

        }
    }
    /// <summary>
    /// Settings structure for XML
    /// </summary>
    [XmlRoot("Settings")]
    public class Settingsxml
    {
        [XmlElement("Bank")]
        public decimal bankowns;

        [XmlElement(ElementName = "playersTurn")]
        public string playersTurn;

        [XmlArray("Players")]
        [XmlArrayItem("Player")]
        public List<PlayerSettings> Players;

        public Settingsxml()
        {

        }

    }
    public class PlayerSettings
    {

        

        [XmlElement(ElementName = "playersLocation", DataType = "int")]
        public int playersLocation;

        [XmlElement(ElementName = "getOutOfJailCard", DataType = "boolean")]
        public bool getOutOfJailCard;

        [XmlElement(ElementName = "inJail", DataType = "boolean")]
        public bool inJail;

        [XmlElement("AccountBalance")]
        public decimal playersAccount;

        [XmlArray(ElementName = "PropertiesOwned")]
        public List<propdetails> PropertiesOwned;

        //public String[] PropertiesOwned;

        // private ArrayList properties;
        [XmlAttributeAttribute(AttributeName = "name")]
        public string playerName;
        public PlayerSettings()
        {
            // Default constructor for serialization.
        }

        public PlayerSettings(string sName, decimal playersAccount, int playersLocation, bool getOutOfJailCard, bool inJail, List<propdetails> args)
        {
            this.playerName = sName;
            
            this.playersAccount = playersAccount;
            this.PropertiesOwned = args;
            this.playersLocation = playersLocation;
            this.getOutOfJailCard = getOutOfJailCard;
            this.inJail = inJail;
        }
    }

   
    public class propdetails {

        [XmlElement(ElementName = "name")]
        public string sName;

        [XmlElement(ElementName = "isMorgaged", DataType = "boolean")]
        public bool isMorgaged;

        [XmlElement(ElementName = "houses", DataType = "int")]
        public int houses;
        public propdetails()
        {
            // Default constructor for serialization.
        }
        public propdetails(string sName, bool isMorgaged , int houses = 0)
        {

            this.sName = sName;
            this.isMorgaged = isMorgaged;
            this.houses = houses;

        }
    }
}

