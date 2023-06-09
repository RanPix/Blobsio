using SFML.Window;

namespace Blobsio.Core;

public struct InputAction
{
    private Action action;
    public Keyboard.Key bind { get; private set; }

    public InputAction(Action invoke, Keyboard.Key bind)
    {
        action = invoke;
        this.bind = bind;
    }

    public void Update()
    {

        if (Keyboard.IsKeyPressed(bind))
        {
            action?.Invoke();
        }
    }
}
