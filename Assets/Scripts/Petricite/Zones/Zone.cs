using System;
using System.Collections.Generic;

namespace Petricite
{
    public class Zone : GenericZone<Card>
    {
       
        public List<Card> cards => heldList;

        public Player owner;

        public bool boardZone;

        public int MaxCards => Query<int, Zone>.GetValue(this, "MAX_CARDS", maxCards);

        public int CardCount => cards.Count;
        public Zone(string name, int maxCards, Player owner, bool boardZone = false) : base(name, maxCards)
        {
            this.owner = owner;
            this.boardZone = boardZone;
        }

    }
}
