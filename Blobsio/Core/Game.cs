global using Time = Blobsio.Core.Time;
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
    private List<Entity> newEntities = new List<Entity>();
    private List<Entity> pendingEntitiesToDestroy = new List<Entity>(); 

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

        while (window.IsOpen)
        {
            Time.Update();

            physics.Update(entities.ToArray());

            input.Update();
            Start();
            Update();
            Destroy();

            renderer.Render(entities.ToArray());
        }
    }

    private void Start()
    {
        if (newEntities.Count == 0)
            return;
        
        for (int i = 0; i < newEntities.Count; i++)
        {
            newEntities[i].Start();
        }

        entities.AddRange(newEntities);
        newEntities.Clear();
    }

    private void Destroy()
    {
        if (pendingEntitiesToDestroy.Count == 0)
            return;
        
        for (int i = 0; i < pendingEntitiesToDestroy.Count; i++)
        {
            entities.Remove(pendingEntitiesToDestroy[i]);
        }

        pendingEntitiesToDestroy.Clear();
    }

    private void Update()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Update();
        }
    }

    private void InitializeEntities()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].OnInstantiate(this);
            entities[i].Start();
        }
    }

    public Entity FindEntitiyByTag(Tag tag)
        => entities.Find(x => x.tag == tag);

    public Entity[] FindEntitiesByTag(Tag tag)
        => entities.FindAll(x => x.tag == tag).ToArray();

    public Entity Instantiate(Entity e)
    {
        newEntities.Add(e);

        e.OnInstantiate(this);

        return e;
    }

    public void Destroy(Entity e)
    {
        pendingEntitiesToDestroy.Add(e);
    }
}
