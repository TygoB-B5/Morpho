using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputController
{
    public abstract Vector2 GetMovementInput();
    public abstract bool IsJumping();
}

public class GamepadInput : InputController
{
    private readonly float Deadzone = 0.1f;

    public override Vector2 GetMovementInput()
    {
        Vector2 input = Gamepad.current.leftStick.value;

        if (Mathf.Abs(input.x) < Deadzone)
            input.x = 0;
        if (Mathf.Abs(input.y) < Deadzone)
            input.y = 0;

        return input;
    }

    public override bool IsJumping()
    {
        return Gamepad.current.buttonSouth.value > 0;
    }
}


public class KeyboardInput : InputController
{
    public override Vector2 GetMovementInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float len = input.magnitude;

        return input.normalized * len;
    }

    public override bool IsJumping()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}

public class GameManager : PersistentSingleton<GameManager>
{
    public static InputController InputManager => Instance.inputManager;
    public static bool IsUsingController => Instance.isUsingController;


    private InputController inputManager;
    private bool isUsingController;

    private void Awake()
    {
        StartCoroutine(PingControllers());
    }

    private IEnumerator PingControllers()
    {
        isUsingController = Gamepad.current != null;
        bool hasController = Gamepad.current != null;

        while (true)
        {
            isUsingController = Gamepad.current != null;
            if (isUsingController)
            {
                if(hasController)
                    inputManager = new GamepadInput();

                hasController = false;
            }
            else
            {
                if(!hasController)
                    inputManager = new KeyboardInput();

                hasController = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
