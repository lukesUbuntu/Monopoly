using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyGame_9901623
{
    /// <summary>
    /// CommunityCards provides cards as a deck and draws certain actions when landed on props
    /// </summary>
    public class ChanceCards
    {
         //http://stackoverflow.com/a/3767989/1170430
        protected static ChanceCards chancecards;
        protected Queue<Action> the_deck;
        protected Player the_player;
        protected String the_action_message = "";
        protected List<Action> randomize_cards = new List<Action>();
        protected int cardCount = 0;
        protected bool removeJailCard = false;
        protected Random randomNumber = new Random();
        public static ChanceCards access()
        {
            if (chancecards == null)
                chancecards = new ChanceCards();

            return chancecards;
        }

        protected ChanceCards()
        {
            
            
            // CountIt testDel = (int x) => x * 5 //This is entire function call;

            the_deck = new Queue<Action>();

            //Advance to Go (Collect $200) 
            randomize_cards.Add(() => advance_to_go());

            //Advance to random property. 
            randomize_cards.Add(() => advance_random());

            //Advance token to nearest Utility. If unowned, you may buy it from the Bank. 
            randomize_cards.Add(() => advance_to_utility());

            //Advance token to the nearest Railroad
            randomize_cards.Add(() => advance_to_transport());

            //Bank pays you dividend of $50 
            randomize_cards.Add(() => bank_pays_dividend());

            //Get out of Jail free – this card may be kept until needed
            randomize_cards.Add(() => get_jail_free());

            //Go back 3 spaces 
            randomize_cards.Add(() => go_back_3_spaces());
            
            //Go directly to Jail – do not pass Go, do not collect $200 
            randomize_cards.Add(() => go_to_jail());


            //Make general repairs on all your property – for each house pay $25
            randomize_cards.Add(() => street_repairs());

            //Pay poor tax of $15 
            randomize_cards.Add(() => pay_poor_tax());
            
            //You have been elected chairman of the board – pay each player $50 
            randomize_cards.Add(() => elected_chair_person());

            //Your building loan matures – collect $150 
            randomize_cards.Add(() => building_loan_matures());

            //You have won a crossword competition - collect $100
            randomize_cards.Add(() => cross_word_comp());
         

           // randomize_cards
            Shuffle<Action>(randomize_cards);

            foreach (Action action in randomize_cards)
            {
                the_deck.Enqueue(action);
            }
        }
       

        public static IList<T> Shuffle<T>(IList<T> list)
        {
            Random Rnd = new Random();
            if (list == null)
                throw new ArgumentNullException("list");

            for (int j = list.Count; j >= 1; j--)
            {
                int item = Rnd.Next(0, j);
                if (item < j - 1)
                {
                    var t = list[item];
                    list[item] = list[j - 1];
                    list[j - 1] = t;
                }
            }
            return list;
        }
        public void shuffleCards()
        {
            Shuffle<Action>(randomize_cards);
           
                //"**** Shuffled community cards ****"
            Console.WriteLine(ConsoleOveride.colorString("**** Shuffled chance cards ****"));
        }
        public String draw_card(ref Player player)
       {
           //check if we haven't been dealt all the cards
           if (cardCount >= the_deck.Count)
               this.shuffleCards();

           cardCount++;
           the_player = player;
           Action action = the_deck.Dequeue();
           action.Invoke();
           return String.Format("<color:White>{0}</color>", this.the_action_message);      
       }
        
        public void advance_to_go()
       {
           the_player.setLocation(0);
           the_action_message = String.Format("Advance straight to GO");
            //add back to the que
            the_deck.Enqueue(() => advance_to_go());
       }

        /// <summary>
        /// Advances to a random prop on board that user is currently not on
        /// </summary>
        public void advance_random()
          {
             
              int current_location = this.the_player.getLocation();
              int location = 0;
              do
              {
                  location = randomNumber.Next(0, Board.access().getProperties().Count);

              } while (location == current_location);

              Property theProp = Board.access().getProperty(location);
              the_action_message = String.Format("Advance to {0}", theProp.getName());
              the_player.setLocation(location);
              the_deck.Enqueue(() => advance_random());
          }


        public void advance_to_utility()
        {
            //loop all props
            ArrayList theProps = Board.access().getProperties();
            Property theUtility = null;
            int location = 0;

            //for(Property theProp in theProps){
            for (int propindex = 0; propindex < theProps.Count; propindex++)
            {
                Property theProp = (Property)theProps[propindex];

                if (theProp is Utility)
                {
                    location = propindex;
                    theUtility = theProp;
                    break;
                }
            }

            if (theUtility == null)
            {
                //damm maybe not added to our properties list we will draw a new card
                this.draw_card(ref the_player);
                return;
            }
            the_player.setLocation(location);
            the_action_message = String.Format("{0} Advance token to nearest Utility. If unowned, you may buy it from the Bank\nNearest Utility is : ", the_player.getName(), theUtility.getName());
        }
        /// <summary>
        /// Advance to the nearest transport
        /// </summary>
        public void advance_to_transport()
          {
              //loop all props
              ArrayList theProps = Board.access().getProperties();
              Property theTransport = null;
              int location = 0;
              //for(Property theProp in theProps){
              for (int propindex = 0; propindex < theProps.Count; propindex++)
            {
                Property theProp = (Property)theProps[propindex];
            
                  if (theProp is Transport)
                  {
                      location = propindex;
                      theTransport = theProp;
                      break;
                  }
              }

              if (theTransport == null)
              {
                  //damm maybe not added to our properties list we will draw a new card
                  this.draw_card(ref the_player);
                  return;
              }
              the_player.setLocation(location);
              the_action_message = String.Format("{0} Advance token to nearest Transport. If unowned, you may buy it from the Bank\nNearest Transport is : ", the_player.getName(), theTransport.getName());
          }




        public void bank_pays_dividend()
        {
            the_action_message = String.Format("Bank pays you dividend of <color:Yellow>$</color>50 ");
            Banker.access().pay(50);
            the_player.receive(50);
            the_deck.Enqueue(() => bank_pays_dividend());
        }



        public void get_jail_free()
          {
              if (this.removeJailCard == true)
              {
                  //give player another card
                  draw_card(ref the_player);
                  return;
              }
              //Set the_player to own community_jail_card
              the_player.giveGetOutJailCard();
              the_action_message = String.Format("Get out of jail free card received\nCard has been removed from deck and is with {0}",the_player.getName());
          }

        public void go_back_3_spaces()
        {
            int location = the_player.getLocation() - 3;

            //make sure we don't go -0
            if (location <= 0)
                location = Board.access().getSquares() - Math.Abs(location);

            the_player.setLocation(location);
            Property theProp = Board.access().getProperty(location);
            the_action_message = String.Format("Go back 3 spaces, new location : {0}", theProp.getName());
        }


        public void go_to_jail()
          {
              //Send player to jail
            
                
              the_deck.Enqueue(() => go_to_jail());
              the_action_message = String.Format("Go straight to jail do not pass go.", the_player.getName());
              the_player.setIsInJail();
              
          }



        public void street_repairs()
          {
              
              //lets get list of all props owned
              decimal repair_cost = 0;
              ArrayList players_props = the_player.getPropertiesOwnedFromBoard();
              foreach (Residential theProp in players_props)
              {
                  int getHouses = theProp.getHouseCount();
                  repair_cost+= getHouses * 25;
              }

              Banker.access().receive(repair_cost);
              the_player.pay(repair_cost);
              the_action_message = String.Format("Make general repairs on all your property – for each house pay <color:Yellow>$</color>25  total cost : <color:Yellow>$</color>{0}", repair_cost);
              the_deck.Enqueue(() => street_repairs());
          }

        public void pay_poor_tax()
        {
            the_action_message = String.Format("Pay poor tax <color:Yellow>$</color>20.00");
            Banker.access().receive(20);
            the_player.pay(20);
            the_deck.Enqueue(() => pay_poor_tax());
        }

        public void elected_chair_person()
        {
            the_action_message = String.Format("You have been elected chairman of the board – pay each player <color:Yellow>$</color>50  ");
            foreach (Player player in Board.access().getPlayers())
            {
                if (player != the_player)
                {
                    player.receive(50);
                    the_player.pay(50);
                }
                
            }
            the_deck.Enqueue(() => elected_chair_person());
        }

        public void building_loan_matures()
        {
            the_action_message = String.Format("Your building loan matures – collect <color:Yellow>$</color>150 ");
            Banker.access().pay(150);
            the_player.receive(150);
            the_deck.Enqueue(() => building_loan_matures());
        }

        public void cross_word_comp()
        {
            the_action_message = String.Format("You have won a crossword competition - collect <color:Yellow>$</color>100");
            Banker.access().pay(100);
            the_player.receive(100);
            the_deck.Enqueue(() => cross_word_comp());
        }
        public void return_jail_card()
          {
              Console.WriteLine("<color:Red>Returned Jail Card to pack</color>");
              the_deck.Enqueue(() => get_jail_free());
              this.removeJailCard = false;
          }

        /// <summary>
        /// removes the jail card from que
        /// </summary>
        public void remove_jail_card()
        {
            Console.WriteLine("<color:Red>Removed Jail Card From pack</color>");
            this.removeJailCard = true;
        }
    }
}
