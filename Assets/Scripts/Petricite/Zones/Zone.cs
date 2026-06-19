using System;
using System.Collections.Generic;

namespace Petricite
{
    public class Zone : IChoosable
    {
        public string Name => name;

        public string name;
        private int maxCards;
        public List<Card> cards;

        public Player owner;

        public int MaxCards => Query<int, Zone>.GetValue(this, "MAX_CARDS", maxCards);

        public int CardCount => cards.Count;
        public Zone(string name, int maxCards, Player owner)
        {
            this.owner = owner;
            this.name = name;
            this.maxCards = maxCards;

            cards = new();
        }

        public void TryAddCard(Card card)
        {

        }
    }
}
