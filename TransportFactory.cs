using System;
using System.Collections.Generic;
using System.Text;

namespace MonopolyGame_9901623
{
    public class TransportFactory : ResidentialFactory
    {
        public Transport create(string sName, IEnum.PropertyGroup dGroup = IEnum.PropertyGroup.NONE)
        {
            return new Transport(sName, dGroup);
        }
    }
}
