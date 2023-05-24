using SFML.System;
using SFML.Window;
using Blobsio.Core.Extentions;

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
        //MovementInput.Invoke(GetKeyboardMovement());
        MovementInput.Invoke(GetMouseMovementInput());
    }

    private Vector2f GetKeyboardMovement()
    {
        Vector2f input = new Vector2f();

        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            input += new Vector2f(-1, 0);

        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            input += new Vector2f(1, 0);


        if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            input += new Vector2f(0, -1);

        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            input += new Vector2f(0, 1);

        return input.Normalize();
    }

    private Vector2f GetMouseMovementInput()
    {
        // не зважайте
        Vector2f input = ((Vector2f)(Mouse.GetPosition(window) - (Vector2i)window.Size / 2)).Normalize();

        return input;
    }
}
