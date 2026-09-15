using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class InputListener : IDisposable
    {
        public event Action OnStartPerformed;
        public event Action OnJumpPressed;
        public event Action OnJumpReleased;

        private readonly GameInput _inputActions;

        public InputListener()
        {
            _inputActions = new GameInput();

            _inputActions.UI.Start.performed += OnStartActionPerformed;

            _inputActions.Player.Jump.performed += OnJumpActionPerformed;
            _inputActions.Player.Jump.canceled += OnJumpActionCanceled;

            _inputActions.UI.Enable();
        }

        public void Dispose()
        {
            _inputActions.UI.Start.performed -= OnStartActionPerformed;
            _inputActions.Player.Jump.performed -= OnJumpActionPerformed;
            _inputActions.Player.Jump.canceled -= OnJumpActionCanceled;
            _inputActions.UI.Disable();
        }

        public void SwitchActionMap(ActionMap actionMap)
        {
            switch (actionMap)
            {
                case ActionMap.Player:
                    _inputActions.Player.Enable();
                    _inputActions.UI.Disable();
                    break;
                case ActionMap.UI:
                    _inputActions.UI.Enable();
                    _inputActions.Player.Disable();
                    break;
            }
        }

        private void OnStartActionPerformed(InputAction.CallbackContext obj)
        {
            OnStartPerformed?.Invoke();
        }

        private void OnJumpActionPerformed(InputAction.CallbackContext obj)
        {
            OnJumpPressed?.Invoke();
        }

        private void OnJumpActionCanceled(InputAction.CallbackContext obj)
        {
            OnJumpReleased?.Invoke();
        }
    }

    public enum ActionMap
    {
        Player,
        UI
    }
}