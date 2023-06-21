using SFML.System;
using SFML.Window;
using Blobsio.Core.Extentions;

namespace Blobsio.Core;

public class Input
{
    private Window window;

    private static List<InputKey> Inputs = new List<InputKey>();

    public void Start(Window window)
    {
        this.window = window;
    }

    public void Update()
    {
        window.DispatchEvents();

        foreach (var action in Inputs)
        {
            action.Update();
        }

        GetMovementInput();
        //GetFoodThrowInput();
    }

    public static InputKey Create(Action input, Keyboard.Key bind)
    {
        InputKey inputKey = new InputKey(bind);
        Inputs.Add(inputKey);
        return inputKey;
    }

    public static void Remove(Keyboard.Key bind)
    {
        for (int i = 0; i < Inputs.Count; i++)
        {
            if (Inputs[i].bind == bind)
                Inputs.RemoveAt(i);
        }
    }

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
}
