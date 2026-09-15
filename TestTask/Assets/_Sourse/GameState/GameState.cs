namespace GameState
{
    public abstract class GameState : BaseState
    {
        protected GameContext GameContest;
        protected StateMachine StateMachine;
        
        protected GameState(GameContext gameContest, StateMachine stateMachine)
        {
            GameContest = gameContest;
            StateMachine = stateMachine;
        }
    }
}