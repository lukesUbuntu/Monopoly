using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
     public class PropertyFactory
    {
         public Property create(string name)
         {
             return new Property(name);
         }
    }
}
