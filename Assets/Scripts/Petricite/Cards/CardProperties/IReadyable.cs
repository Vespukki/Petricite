using System;
using System.Collections.Generic;

namespace Petricite
{
    public interface IReadyable : ICardProperty
    {
        public bool Ready { get; set; }

        public static event Action<IReadyable, bool> OnReadyChange;

        protected static void InvokeOnReady(IReadyable obj, bool ready)
        {
            OnReadyChange?.Invoke(obj, ready);
        }
    }
}
