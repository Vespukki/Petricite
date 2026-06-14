using NUnit.Framework;
using System.Collections.Generic;

namespace Petricite
{
    public class Player : IChoosable
    {
        private string name;
        public string Name => name;

        private List<Protocard> protoCards;

        public Player(string name, List<Protocard> protoCards)
        {
            this.name = name;
            this.protoCards = protoCards;
        }

    }
}
