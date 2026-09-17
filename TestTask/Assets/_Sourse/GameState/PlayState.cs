using Core;

namespace GameState
{
    public class PlayState : GameState
    {
        public PlayState(GameContext gameContest, StateMachine stateMachine) : base(gameContest, stateMachine)
        {

        }

        public override void Enter()
        {
            GameContest.PlayerController.enabled = true;
            GameContest.PlayerController.OnPlayerDeath += OnDeath;

            GameContest.InputListener.SwitchActionMap(ActionMap.Game);

            GameContest.AsteroidSpawner.enabled = true;
            GameContest.BoosterSpawner.enabled = true;
        }

        public override void Exit()
        {
            GameContest.ScoreManager.ResetScore();

            GameContest.PlayerController.OnPlayerDeath -= OnDeath;
        }

        private void OnDeath()
        {
            StateMachine.ChangeState(new MenuState(GameContest, StateMachine));
        }
    }
}