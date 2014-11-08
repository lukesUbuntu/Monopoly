using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame_9901623
{
    /// <summary>
    /// Testing comunity manager
    /// </summary>
    [TestFixture]
    class _TestCommunityCards
    {
        
     
            [Test]
            public void testDeckShuffling()
            {
                //Draw cards till shuffle is invoked
            }

            [Test]
            public void testGetOutOfFreeJail(){
                //test once we have received card can it be used and put back into deck
                TestCommunityCards testCards = new TestCommunityCards();
                testCards.go_to_jail();
            }
        
    }

    class TestCommunityCards : CommunityCards
    {

        
    }
}
