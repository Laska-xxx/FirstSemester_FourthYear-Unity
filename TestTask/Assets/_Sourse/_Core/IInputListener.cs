using System;

namespace Core
{
    public interface IInputListener
    {
        event Action OnStartPerformed;
        event Action OnJumpPressed;
        event Action OnJumpReleased;
        void SwitchActionMap(ActionMap actionMap);
    }
}