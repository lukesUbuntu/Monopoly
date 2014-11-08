﻿using System;
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
        //https://answers.yahoo.com/question/index?qid=20110528154141AAFwRyu
        //http://stackoverflow.com/a/3767989/1170430
        private static ChanceCards the_manager;
        private Queue<Action> the_deck;
        private Player the_player;
        private String the_action_message = "";
        private List<Action> randomize_cards = new List<Action>();


        public static ChanceCards access()
        {
            if (the_manager == null)
                the_manager = new ChanceCards();

            return the_manager;
        }

        private ChanceCards()
        {


            the_deck = new Queue<Action>();

            // CountIt testDel = (int x) => x * 5 //This is entire function call;
            randomize_cards.Add(() => advance_to_go());

            randomize_cards.Add(() => error_in_favour());

            randomize_cards.Add(() => doctors_fees());
            /*
           randomize_cards.Add(() => get_jail_free());
           randomize_cards.Add(() => go_to_jail());
           randomize_cards.Add(() => happy_birthday());
           randomize_cards.Add(() => grand_opera());
           randomize_cards.Add(() => tax_refund());
           randomize_cards.Add(() => life_insurance());
           randomize_cards.Add(() => pay_hospital_fees());
           randomize_cards.Add(() => pay_school_fees());
           randomize_cards.Add(() => receive_consultancy_fee());
           randomize_cards.Add(() => street_repairs());
           randomize_cards.Add(() => beauty_contest());
           randomize_cards.Add(() => inheritance());
           randomize_cards.Add(() => holiday_fund());
           */

            // randomize_cards
            Shuffle<Action>(randomize_cards);

            foreach (Action action in randomize_cards)
            {
                the_deck.Enqueue(action);
            }
        }
        /*

       public IList Shuffle<T>(IList<T> list)
       {
           Random rand = new Random();
           int n = list.Count;
           while (n > 1)
           {
               n--;
               int k = rand.Next(n + 1);
               T value = list[k];
               list[k] = list[n];
               list[n] = value;
           }

           return (IList)list;
       }
        
       
          */

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
            Console.WriteLine("Shuffled community cards");
        }
        public String draw_card(ref Player player)
        {
            the_player = player;
            Action action = the_deck.Dequeue();
            action.Invoke();
            return this.the_action_message;
            /*
            var actionList = new[]
                 {
                     new Action( () => advance_to_go() ),
                     new Action( () => advance_to_go() ),
                     new Action( () => advance_to_go() )
                 }.ToList();

            var r = new Random();
            while (actionList.Count() > 0)
            {
                var index = r.Next(actionList.Count());
                var actions = actionList[index];
                actionList.RemoveAt(index);
                actions();
            }*/
        }

        private void advance_to_go()
        {
            the_player.setLocation(0);
            the_action_message = String.Format("Recevied Advance to GO");
            //the_deck.Enqueue(() => advance_to_go());
        }


        private void error_in_favour()
        {
            Banker.access().pay(75);
            the_player.receive(75);
            the_deck.Enqueue(() => error_in_favour());
            the_action_message = String.Format("Bank error in your favour received $75.00");
        }

        private void doctors_fees()
        {
            the_player.pay(50);
            Banker.access().receive(50);
            the_deck.Enqueue(() => doctors_fees());
            the_action_message = String.Format("had to pay doctors fees of $50.00");
        }

        private void get_jail_free()
        {
            //Set the_player to own community_jail_card
            Console.WriteLine("Get out of jail free");
        }

        private void go_to_jail()
        {
            //Send player to jail
            Console.WriteLine("Go To Jail");
            the_deck.Enqueue(() => go_to_jail());

        }

        private void happy_birthday()
        {
            Console.WriteLine("Happy birthday");
            foreach (Player player in Board.access().getPlayers())
            {
                player.pay(10);
                the_player.receive(10);
            }
            the_deck.Enqueue(() => happy_birthday());
        }

        private void grand_opera()
        {
            Console.WriteLine("Grand opera");
            foreach (Player player in Board.access().getPlayers())
            {
                player.pay(50);
                the_player.receive(50);
            }
            the_deck.Enqueue(() => grand_opera());
        }

        private void tax_refund()
        {
            Console.WriteLine("Tax refund");
            Banker.access().pay(75);
            the_player.receive(75);
            the_deck.Enqueue(() => tax_refund());
        }

        private void life_insurance()
        {
            Console.WriteLine("Life insurance");
            Banker.access().pay(100);
            the_player.receive(100);
            the_deck.Enqueue(() => life_insurance());
        }

        private void pay_hospital_fees()
        {
            Console.WriteLine("Hospital Fees");
            the_player.pay(100);
            Banker.access().receive(100);
            the_deck.Enqueue(() => pay_hospital_fees());
        }

        private void pay_school_fees()
        {
            Console.WriteLine("pay_school_fees");
            the_player.pay(50);
            Banker.access().receive(50);
            the_deck.Enqueue(() => pay_school_fees());
        }

        private void receive_consultancy_fee()
        {
            Console.WriteLine("receive_consultancy_fee");
            Banker.access().pay(25);
            the_player.receive(25);
            the_deck.Enqueue(() => life_insurance());
        }

        private void street_repairs()
        {
            Console.WriteLine("street_repairs");
            //Count houses/hotels on all owned properties
            Banker.access().receive(100);
            the_player.pay(100);
            the_deck.Enqueue(() => street_repairs());
        }

        private void beauty_contest()
        {
            Console.WriteLine("beauty_contest");
            Banker.access().pay(10);
            the_player.receive(10);
            the_deck.Enqueue(() => beauty_contest());
        }

        private void inheritance()
        {
            Console.WriteLine("inheritance");
            Banker.access().pay(100);
            the_player.receive(100);
            the_deck.Enqueue(() => inheritance());
        }

        private void holiday_fund()
        {
            Console.WriteLine("holiday_fund");
            Banker.access().pay(100);
            the_player.receive(100);
            the_deck.Enqueue(() => holiday_fund());
        }

        private void return_jail_card()
        {
            Console.WriteLine("return_jail_card");
            the_deck.Enqueue(() => get_jail_free());
        }
    }
}
