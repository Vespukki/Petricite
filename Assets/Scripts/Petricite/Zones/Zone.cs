using System;
using System.Collections.Generic;

namespace Petricite
{
    public class Zone : IChoosable
    {
        public string ChoiceName => name;

        public string name;
        private int maxCards;
        private List<Card> cards;

        public int MaxCards => Query<int, Zone>.GetValue(this, "MAX_CARDS", maxCards);

        public int CardCount => cards.Count;
        public Zone(string name, int maxCards)
        {
            this.name = name;
            this.maxCards = maxCards;

            cards = new();
        }

        public void TryAddCard(Card card)
        {

        }
    }
}
