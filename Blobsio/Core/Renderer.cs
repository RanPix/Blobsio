using SFML.Graphics;
using SFML.Window;

namespace Blobsio.Core;

public class Renderer
{
    private RenderWindow window;
    private Color windowColor;

    public static uint windowX = 800;
    public static uint windowY = 1200;

    public void Start(RenderWindow window)
    {
        this.window = window;

        window.Closed += new EventHandler(OnClose);
        window.Resized += new EventHandler<SizeEventArgs>(OnResize);

        window.SetFramerateLimit(165);

        windowColor = new Color(0, 0, 0);
    }

    private void OnResize(object sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender;
        window.Size = new SFML.System.Vector2u(windowX, windowY);
    }

    private void OnClose(object sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender;
        window.Close();
    }

    public void Render(Entity[] objects)
    {
        if (!window.IsOpen)
            return;

        window.Clear(windowColor);

        Draw(objects);

        window.Display();
    }

    private void Draw(Entity[] objects)
    {
        foreach (Entity obj in objects) 
        {
            if (obj.graphic is Drawable)
                window.Draw((Drawable)obj.graphic);
        }
    }
}
