using Core;

namespace GameState
{
    public class MenuState : GameState
    {
        public MenuState(GameContext gameContest, StateMachine stateMachine) : base(gameContest, stateMachine)
        {

        }

        public override void Enter()
        {
            GameContest.PlayerController.enabled = false;
            GameContest.AsteroidFactory.enabled = false;
            GameContest.BoosterFactory.enabled = false;
            GameContest.InputListener.SwitchActionMap(ActionMap.UI);
            GameContest.InputListener.OnStartPerformed += StartGame;
        }

        public override void Exit()
        {
            GameContest.InputListener.OnStartPerformed -= StartGame;
        }

        private void StartGame()
        {
            StateMachine.ChangeState(new PlayState(GameContest, StateMachine));
        }
    }
}