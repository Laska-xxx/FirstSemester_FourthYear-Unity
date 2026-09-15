namespace GameState
{
    public class PlayState : GameState
    {
        public PlayState(GameContext gameContest, StateMachine stateMachine) : base(gameContest, stateMachine)
        {
        }
        public override void Enter()
        {
            // Handle entering the play state logic here
        }
        public override void Update()
        {
        }
        public override void Exit()
        {
            // Handle exiting the play state logic here
        }

        private void GameOver()
        {
            // Handle game over logic here
        }
    }
}