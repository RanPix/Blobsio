global using Time = Blobsio.Core.Time;
using Blobsio.Core.Interfaces;
using SFML.Graphics;
using SFML.Window;

namespace Blobsio.Core;

public class Game
{
    private RenderWindow window = new RenderWindow(new VideoMode(Renderer.windowX, Renderer.windowY), "PingPong");

    private Renderer renderer = new Renderer();
    private Physics physics = new Physics();
    private Input input = new Input();

    public const float MAP_SIZE = 5000f;

    private List<Entity> entities = new List<Entity>();
    private List<IUpdatable> updatables = new List<IUpdatable>();
    private List<IDrawable> drawables = new List<IDrawable>();

    private List<Entity> newEntities = new List<Entity>();
    private List<IUpdatable> newUpdatables = new List<IUpdatable>();
    private List<IDrawable> newDrawables = new List<IDrawable>();

    public Game(List<Entity> entities)
    {
        this.entities = entities;
    }


    public void Run()
    {
        Time.Start();
        renderer.Start(window);
        input.Start(window);

        InitializeEntities();
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
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Start();
        }
    }

    private void Update()
    {
        for (int i = 0; i < updatables.Count; i++)
        {
            updatables[i].Update();
        }
    }

    private void InitializeEntities()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].OnInstantiate(this);

            if (entities[i] is IUpdatable)
                updatables.Add((IUpdatable)entities[i]);

            if (entities[i] is IDrawable)
                drawables.Add((IDrawable)entities[i]);
        }
    }

    public Entity FindEntitiyByTag(Tag tag)
        => entities.Find(x => x.tag == tag);

    public Entity[] FindEntitiesByTag(Tag tag)
        => entities.FindAll(x => x.tag == tag).ToArray();

    public Entity Instantiate(Entity e)
    {
        entities.Add(e);
        
        if (e is IUpdatable)
            updatables.Add((IUpdatable)e);

        if (e is IDrawable)
            drawables.Add((IDrawable)e);

        e.Start();
        e.OnInstantiate(this);

        return e;
    }

    public void Destroy(Entity e)
    {
        e.OnDestroy();

        entities.Remove(e);

        if (e is IUpdatable)
            updatables.Remove((IUpdatable)e);

        if (e is IDrawable)
            drawables.Remove((IDrawable)e);
    }
}
