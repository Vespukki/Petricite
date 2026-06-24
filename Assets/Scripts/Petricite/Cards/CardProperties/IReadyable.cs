using System;
using System.Collections.Generic;

namespace Petricite
{
    public interface IReadyable : ICardProperty
    {
        public bool Ready { get; set; }

        public static event Action<IReadyable, bool> OnReady;

        public List<Ability> Abilities { get; }

    }
}
