using System;

namespace GameState
{
    public class StateMachine
    {
        public BaseState CurrentState { get; private set; }
        public Action<BaseState> OnStateChanged;

        public void ChangeState(BaseState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();

            OnStateChanged?.Invoke(CurrentState);
        }
    }
}