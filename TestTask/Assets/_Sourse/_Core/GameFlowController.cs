using GameState;
using System;
using VContainer;
using VContainer.Unity;

public class GameFlowController : IInitializable, IDisposable
{
    private readonly StateMachine _stateMachine;
    private readonly GameContext _gameContext;

    [Inject] public GameFlowController(StateMachine stateMachine, GameContext gameContext)
    {
        _stateMachine = stateMachine;
        _gameContext = gameContext;
    }

    public void Initialize()
    {
        _stateMachine.ChangeState(new MenuState(_gameContext, _stateMachine));
    }

    public void Dispose()
    {
        _stateMachine.ChangeState(null);
    }
}
