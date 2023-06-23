using Blobsio.Core.Entities;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Blobsio.Core;

public class Renderer
{
    private RenderWindow window;
    private Color windowColor;

    public static View mainCamera = new View();
    private static Vector2f cameraPivot;

    public static uint windowX = 1280;
    public static uint windowY = 720;

    private int fps;
    private float time;

    public void Start(RenderWindow window)
    {
        this.window = window;
        windowX = window.Size.X; 
        windowY = window.Size.Y;

        window.Closed += new EventHandler(OnClose);
        window.Resized += new EventHandler<SizeEventArgs>(OnResize);

        window.SetFramerateLimit(165);

        mainCamera.Reset(new FloatRect(0, 0, windowX, windowY));

        mainCamera.Viewport = new FloatRect(0f, 0f, 1f, 1f);

        windowColor = new Color(0, 0, 0);
    }

    private void OnResize(object sender, EventArgs e)
    {
        RenderWindow window = (RenderWindow)sender;
        windowX = window.Size.X;
        windowY = window.Size.Y;

        mainCamera.Size = (Vector2f)window.Size;
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

        CalculateFPS();

        window.SetView(mainCamera);
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


    private void CalculateFPS()
    {
        fps++;
        time += Time.deltaTime;

        if (time > 1f)
        {
            window.SetTitle(fps.ToString());

            fps = 0;
            time = 0;
        }

    }
}
