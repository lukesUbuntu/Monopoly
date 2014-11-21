using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 */
namespace MonopolyGame_9901623
{
    /// <summary>
    /// CommunityCards provides cards as a deck and draws certain actions when landed on props
    /// 
    /// </summary>
    public class CommunityCards 
    {
        protected static CommunityCards communitycards;
        protected Queue<Action> the_deck;
        protected Player the_player;
        protected String the_action_message = "";
        protected List<Action> randomize_cards = new List<Action>();
        protected int cardCount = 0;
        protected bool removeJailCard = false;
        public static CommunityCards access()
        {
            if (communitycards == null)
                communitycards = new CommunityCards();

            return communitycards;
        }

        protected CommunityCards()
        {
            
            
            // CountIt testDel = (int x) => x * 5 //This is entire function call;

            the_deck = new Queue<Action>();

            //Advance to Go (Collect $200) 
            randomize_cards.Add(() => advance_to_go());

            //Bank error in your favor – collect $75 
            randomize_cards.Add(() => bank_error_in_favour());

            //Doctor's fees – Pay $50 
            randomize_cards.Add(() => doctors_fees());

            //Get out of jail free – this card may be kept until needed
            randomize_cards.Add(() => get_jail_free());

            //Go to jail – go directly to jail – Do not pass Go, do not collect $200 
            randomize_cards.Add(() => go_to_jail());

            //It is your birthday Collect $10 from each player 
            randomize_cards.Add(() => your_birthday());

            //Grand Opera Night – collect $50 from every player for opening night seats 
            randomize_cards.Add(() => grand_opera_night());

            //Income Tax refund – collect $20 
            randomize_cards.Add(() => income_tax_refund());

            //Life Insurance Matures – collect $100 
            randomize_cards.Add(() => life_insurance());

            //Pay Hospital Fees of $100 
             randomize_cards.Add(() => pay_hospital_fees());

            //Pay School Fees of $50 
            randomize_cards.Add(() => pay_school_fees());

            //Receive $25 Consultancy Fee 
            randomize_cards.Add(() => receive_consultancy_fee());

            //You are assessed for street repairs – $40 per house, $115 per hotel 
            randomize_cards.Add(() => street_repairs());

            //You have won second prize in a beauty contest– collect $10 
           randomize_cards.Add(() => beauty_contest());

            //You inherit $100 
           randomize_cards.Add(() => inheritance());

            //From sale of stock you get $50 
            randomize_cards.Add(() => sale_of_stock());
            
            //Holiday Fund matures - Receive $100 
           randomize_cards.Add(() => holiday_fund());


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
            Console.WriteLine(ConsoleOveride.colorString("**** Shuffled community cards ****"));
        }
        public virtual string draw_card(ref Player player)
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

        public void bank_error_in_favour()
          {
              Banker.access().pay(75);
              the_player.receive(75);
              the_deck.Enqueue(() => bank_error_in_favour());
              the_action_message = String.Format("Bank error in your favour {0} receives $75.00", the_player.getName());
          }

        public void doctors_fees()
          {
              the_player.pay(50);
              Banker.access().receive(50);
              the_deck.Enqueue(() => doctors_fees());
              the_action_message = String.Format("{0} pay doctors fees of $50.00", the_player.getName());
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

        public void go_to_jail()
          {
              //Send player to jail
            
                
              the_deck.Enqueue(() => go_to_jail());
              the_action_message = String.Format("Go straight to jail do not pass go.", the_player.getName());
              the_player.setIsInJail();
              
          }

        public void your_birthday()
          {
              the_action_message = String.Format("Its your birthday you collected $10.00 from every player");
              foreach (Player otherPlayer in Board.access().getPlayers())
              {
                  if (otherPlayer != the_player)
                  {
                      otherPlayer.pay(10);
                      //the_action_message +=     "\n"    +   otherPlayer.getName();
                      the_player.receive(10);
                  }

              }
              the_deck.Enqueue(() => your_birthday());
          }

        public void grand_opera_night()
          {
              the_action_message = String.Format("Grand Opera Night collect $50.00 from every player for opening night seats ");
              foreach (Player player in Board.access().getPlayers())
              {
                  if (player != the_player)
                  {
                      player.pay(50);
                      the_player.receive(50);
                  }
                 
              }
              the_deck.Enqueue(() => grand_opera_night());
          }

        public void income_tax_refund()
          {
              the_action_message = String.Format("Income Tax refund – collect $20.00");
              Banker.access().pay(20);
              the_player.receive(20);
              the_deck.Enqueue(() => income_tax_refund());
          }

        public void life_insurance()
          {
              the_action_message = String.Format("Life Insurance Matures – collect $100 ");
              Banker.access().pay(100);
              the_player.receive(100);
              the_deck.Enqueue(() => life_insurance());
          }

        public void pay_hospital_fees()
          {
              the_action_message = String.Format("Pay Hospital Fees of $100");
            
              the_player.pay(100);
              Banker.access().receive(100);
              the_deck.Enqueue(() => pay_hospital_fees());
          }

        public void pay_school_fees()
          {
              the_action_message = String.Format("Pay School Fees of $50");
              the_player.pay(50);
              Banker.access().receive(50);
              the_deck.Enqueue(() => pay_school_fees());
          }

        public void receive_consultancy_fee()
          {
              the_action_message = String.Format("Receive $25 Consultancy Fee");
              Banker.access().pay(25);
              the_player.receive(25);
              the_deck.Enqueue(() => receive_consultancy_fee());
          }

        public void street_repairs()
          {
              
              //lets get list of all props owned
              decimal repair_cost = 0;
              ArrayList players_props = the_player.getPropertiesOwnedFromBoard();
              foreach (Residential theProp in players_props)
              {
                  int getHouses = theProp.getHouseCount();
                  repair_cost+= getHouses * 40;
              }

              Banker.access().receive(repair_cost);
              the_player.pay(repair_cost);
              the_action_message = String.Format("You are assessed for street repairs – $40 per house total cost ${0}", repair_cost);
              the_deck.Enqueue(() => street_repairs());
          }

        public void beauty_contest()
          {
              the_action_message = String.Format("You have won second prize in a beauty contest – collect $10");
              Banker.access().pay(10);
              the_player.receive(10);
              the_deck.Enqueue(() => beauty_contest());
          }

        public void inheritance()
          {
              the_action_message = String.Format("You inherit $100");
              Banker.access().pay(100);
              the_player.receive(100);
              the_deck.Enqueue(() => inheritance());
          }
        public void sale_of_stock()
        {
            the_action_message = String.Format("From sale of stock you get $50");
            Banker.access().pay(50);
            the_player.receive(50);
            the_deck.Enqueue(() => sale_of_stock());
        }

        public void holiday_fund()
          {
              the_action_message = String.Format("Holiday Fund matures - Receive $100");
              Banker.access().pay(100);
              the_player.receive(100);
              the_deck.Enqueue(() => holiday_fund());
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
