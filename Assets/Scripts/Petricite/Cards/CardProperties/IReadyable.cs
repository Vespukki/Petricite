using System;

namespace Petricite
{
    public interface IReadyable : ICardProperty
    {
        public bool Ready { get; set; }

        public static event Action<IReadyable, bool> OnReady;

        public void SetReady(bool isReady)
        {
            OnReady?.Invoke(this, isReady);
            GameAction<bool, IReadyable> newAction = new("READY", this, new() { this }, isReady);
            GameAction<bool, IReadyable>.RaiseBeforeAction(newAction);
        }

    }
}
