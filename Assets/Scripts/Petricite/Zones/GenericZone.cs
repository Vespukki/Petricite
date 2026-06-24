using UnityEngine;
using System.Collections.Generic;

namespace Petricite
{
    public class GenericZone<T> : IChoosable
    {
        public string Name => name;

        protected string name;
        protected int maxCards;
        public List<T> heldList = new();

        public GenericZone(string name, int maxCards)
        {
            this.name = name;
            this.maxCards = maxCards;
        }
    }
}
