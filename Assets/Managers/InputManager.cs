using UnityEngine.InputSystem;

public class InputManager
{
    private static InputActions _actions;

    public static InputActions Actions
    {
        get
        {

            if (_actions == null)
            {
                _actions = new InputActions();
            }
            return _actions;
        }


    }
    public static void SwitchMap(InputActionMap mapToActivate)
    {
        Actions.Disable();
        mapToActivate.Enable();
    }
}