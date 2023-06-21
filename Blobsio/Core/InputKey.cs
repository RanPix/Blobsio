using SFML.Window;

namespace Blobsio.Core;

public class InputKey
{
    public Action onKeyPress;
    public Action onKeyDown;
    public Action onKeyUp;

    public Keyboard.Key bind { get; private set; }

    public InputKey(Keyboard.Key bind)
    {
        this.bind = bind;
    }

    public void Update()
    {
        GetKeyPress();
        GetKeyDown();
        GetKeyUp();
    }

    private void GetKeyPress()
    {

    }

    private void GetKeyUp()
    {

    }

    private void GetKeyDown()
    {

    }
}
