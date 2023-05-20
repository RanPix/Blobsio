global using Time = Blobsio.Core.Time;
using SFML.Graphics;
using SFML.Window;
using SFML_Thing.Core;
using System.ComponentModel;

namespace Blobsio.Core;

public class Game
{
    public static Game instance;

    private RenderWindow window = new RenderWindow(new VideoMode(Renderer.windowX, Renderer.windowY), "PingPong");

    private Renderer renderer = new Renderer();
    private Physics physics = new Physics();
    private Input input = new Input();

    private List<Entity> entities = new List<Entity>();

    public Game(List<Entity> entities)
    {
        if (instance == null)
            instance = this;

        else
            throw new Exception("CANNOT HAVE MORE THAN ONE INSTANCE OF THE GAME");

        this.entities = entities;
    }


    public void Run()
    {
        Time.Start();
        renderer.Start(window);
        input.Start(window);
        Start();

        while (true)
        {
            Time.Update();

            renderer.Render(entities.ToArray());
            physics.Update(entities.ToArray());

            input.Update();
            Update();
        }
    }

    private void Start()
    {
        foreach (Entity entity in entities)
        {
            entity.Start();
        }
    }

    private void Update()
    {
        foreach (Entity entity in entities)
        {
            entity.Update();
        }
    }


    public Entity FindByTag(Tag tag)
        => entities.Find(x => x.tag == tag);
}
