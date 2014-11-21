using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{


    public class Jail : Property
    {
        
        private bool isJail;
        public Jail() : this("Jail") { }

        public Jail(string sName,bool isJail = false)
        {
            this.sName = sName;
            this.isJail = isJail;
        }

        public bool isJailProp(){
            return this.isJail;
        }
        public override string ToString()
        {
            return base.ToString();
        }

        public override string landOn(ref Player player)
        {
            //if this is a jail property and user has past go.
            //&& player.passGo()
            if (this.isJail)
            {
                //player landed on jail
                player.setIsInJail();
                //if is a benefit player receives amount else pay amount
                return base.landOn(ref player) + String.Format("{0} has gone to jail.", player.getName());

            }
            else
            {
                //if is a benefit player receives amount else pay amount
                return base.landOn(ref player) + String.Format("{0} just visting jail.", player.getName());

            }
           
                    
        }
    }
}
