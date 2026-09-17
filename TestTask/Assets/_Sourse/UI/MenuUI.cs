using GameState;
using UnityEngine;
using VContainer;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;

    private StateMachine _stateMachine;

    [Inject] private void Init(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void OnEnable()
    {
        _stateMachine.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _stateMachine.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(BaseState newState)
    {
        _menuPanel.SetActive(newState is MenuState);
    }
}
