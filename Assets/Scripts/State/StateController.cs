using System;

namespace RunningFishes.Pong.State
{
    public class StateController
    {
        /// <summary>
        /// Action to be called when the state changes.
        /// States is the new state.
        /// </summary>
        public event Action<States> OnStateChanged;

        public void RaiseStateChanged(States state)
        {
            OnStateChanged?.Invoke(state);
        }

        public StateController()
        {
            RaiseStateChanged(States.Prestart);
        }

        public void Dispose()
        {
            // Do nothing
        }
    }
}