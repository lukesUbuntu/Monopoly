using System.Collections;
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
    }
}
