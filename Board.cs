using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace MonopolyGame_9901623 
{
    /// <summary>
    /// This is class for singleton Board that has properties and traders on it.
    /// </summary>

    public class Board 
    {
        //provide an static instance of this class to create singleton
        static Board board;
        private ArrayList properties;
        //private ArrayList propertyGroups;
        private ArrayList players;
        private Settings theSettings = new Settings();

        //comunity cards
        private ArrayList community;

        int SQUARES = 40;
     
        //method to access singleton
        public static Board access()
        {
            if (board == null)
                board = new Board();
            return board;
        }
        
        public Board()
        {
            properties = new ArrayList(this.getSquares());
            players = new ArrayList();
            community = new ArrayList();

        }


        public int getSquares()
        {
            return this.SQUARES;
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public void addPlayer(Player player)
        {
            players.Add(player);
        }
        public void setSquares()
        {
            //set the squares of the board
            //@todo need to test this out for 
            this.SQUARES = this.properties.Count;
        }
        public void addProperty(Property property)
        {
            this.properties.Add(property);
        }
        /*
        public void addCommunityCard(CommunityCards communitycard)
        {
            this.community.Add(communitycards);
        }

        public void getCommunityCard()
        {
            return (CommunityCards)properties[propIndex];
        }
         */
        public int getPlayerCount()
        {
            return players.Count;
        }

       

        public Player getPlayer(int playerIndex)
        {
            return (Player)players[playerIndex];
        }
        
        /// <summary>
        /// Returns the complete list of properties in a group from the properties array
        /// </summary>
        /// <param name="theGroup">Game.PropertyGroup requires property group</param>
        /// <returns>Property arraylist</returns>
        public ArrayList getPropGroups(Game.PropertyGroup theGroup)
        {
            ArrayList tmpProps = new ArrayList();
            //return (Player)players[playerIndex];
            for (int i = 0; i < Board.access().getProperties().Count; i++)
            {
                Game.PropertyGroup AlltheGroup = Board.access().getProperty(i).getGroup();


                //owned by this player
                Property thisProperty = Board.access().getProperty(i);
                if (thisProperty.getGroup() == theGroup)
                {
                    //add to arraylist
                    tmpProps.Add(thisProperty);

                }
            }

            return tmpProps;
        }

        /// <summary>
        /// Counts the total amount in a group of properties
        /// </summary>
        /// <param name="theGroup">Game.PropertyGroup</param>
        /// <returns>int of groups </returns>
        public int countPropGroups(Game.PropertyGroup theGroup)
        {
            return getPropGroups(theGroup).Count;
        }

        public Player getPlayer(string sName)
        {
            foreach (Player p in players)
            {
                if (p.getName() == sName)
                    return p;
            }

            // if no players with that name return null
            return null;
        }

        public Property getProperty(int propIndex)
        {
            return (Property)properties[propIndex];
        }

        public ArrayList getPlayers()
        {
            return this.players;
        }

        public ArrayList getProperties()
        {
            return this.properties;
        }

        public void saveGame()
        {
            theSettings.save();
        }
        public void loadGame()
        {
            theSettings.load();
        }
    }
}
