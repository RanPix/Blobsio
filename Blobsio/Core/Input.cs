using SFML.System;
using SFML.Window;

namespace Blobsio.Core;

public class Input
{
    private Window window;

    public static Action<Vector2f> MovementInput;

    public void Start(Window window)
    {
        this.window = window;
    }

    public void Update()
    {
        window.DispatchEvents();

        GetMovementInput();
    }


    private void GetMovementInput()
    {
        MovementInput.Invoke(GetKeyboardMovement());
    }

    private Vector2f GetKeyboardMovement()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            return new Vector2f(-1, 0);

        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            return new Vector2f(1, 0);

        return new Vector2f(0, 0);
    }
}
