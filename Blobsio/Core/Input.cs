using SFML.System;
using SFML.Window;
using Blobsio.Core.Extentions;
using System.Runtime.CompilerServices;

namespace Blobsio.Core;

public class Input
{
    private Window window;

    public void Start(Window window)
    {
        this.window = window;
    }

    public void Update()
    {
        window.DispatchEvents();

        GetMovementInput();
        GetFoodThrowInput();
    }

    #region MovementInput

    public static Action<Vector2f> MovementInput;

    private void GetMovementInput()
    {
        if (MovementInput == null)
            return;

        Vector2f input = new Vector2f();

        //input = GetKeyboardMovement();
        input = GetMouseMovementInput();

        if (input != new Vector2f())
            MovementInput.Invoke(input);
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

    private Vector2f GetMouseMovementInput() // не зважайте
        => ((Vector2f)(Mouse.GetPosition(window) - (Vector2i)window.Size / 2)).Normalize();

    #endregion

    #region FoodThrowInput

    public static Action FoodThrowInput;

    private void GetFoodThrowInput()
    {
        if (FoodThrowInput == null)
            return;

        bool input = false;

        input = GetKeyboardFoodThrow();

        if (input)
        {
            FoodThrowInput.Invoke();
            return;
        }
    }

    private bool GetKeyboardFoodThrow()
        => Keyboard.IsKeyPressed(Keyboard.Key.F);

    #endregion
}
