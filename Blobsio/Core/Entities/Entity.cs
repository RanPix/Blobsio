using SFML.Graphics;
using SFML.System;

namespace Blobsio.Core.Entities;

public class Entity
{
    internal Game world { get; private set; }

    public Tag<string> tag = new Tag<string>();
    public Transformable graphic = new Transformable();

    public float collider;
    public bool processCollision = true;

    private Vector2f _position;
    public Vector2f position
    {
        get { return _position; }
        set
        {
            _position = value;
            graphic.Position = value;
        }
    }

    protected List<Component> components = new List<Component>();
    protected List<Component> newComponents = new List<Component>(); // зробив це протектами по приколу
    protected List<Component> pendingComponentsToDestroy = new List<Component>();

    #region ctor

    public Entity() { }

    public Entity(List<Component> components)
    {
        newComponents = components;
    }

    public Entity(List<Component> components, Transformable graphic)
    {
        newComponents = components;
        this.graphic = graphic;
    }

    public Entity(string tag = "", float collider = 0f, bool processCollision = true, Vector2f position = new Vector2f())
    {
        this.tag += tag;
        this.collider = collider;
        this.processCollision = processCollision;
        this.position = position;
    }
    public Entity(List<Component> components, string tag = "", float collider = 0f, bool processCollision = true, Vector2f position = new Vector2f())
    {
        newComponents = components;
        this.tag += tag;
        this.collider = collider;
        this.processCollision = processCollision;
        this.position = position;
    }

    public Entity(List<Component> components, Transformable graphic, string tag = "", float collider = 0f, bool processCollision = true, Vector2f position = new Vector2f())
    {
        newComponents = components;
        this.graphic = graphic;
        this.tag += tag;
        this.collider = collider;
        this.processCollision = processCollision;
        this.position = position;
    }

    #endregion

    public void OnInstantiate(Game g)
    {
        world = g;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        for (int i = 0; i < newComponents.Count; i++)
        {
            newComponents[i].OnInstantiate(this);
        }
    }

    public virtual void Start()
    {
        AddNewComponents();
    }

    public virtual void Update()
    {
        AddNewComponents();

        for (int i = 0; i < components.Count; i++)
        {
            components[i].Update();
        }
    }

    public virtual void LateUpdate()
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].LateUpdate();
        }
    }

    internal void AddNewComponents()
    {
        if (newComponents.Count == 0)
            return;

        components.AddRange(newComponents);

        for (int i = 0; i < newComponents.Count; i++)
        {
            newComponents[i].Start();
        }

        newComponents.Clear();
    }

    internal void Destroy()
    {
        if (pendingComponentsToDestroy.Count == 0)
            return;

        for (int i = 0; i < pendingComponentsToDestroy.Count; i++)
        {
            pendingComponentsToDestroy[i].OnDestroy();
            components.Remove(pendingComponentsToDestroy[i]);
        }

        pendingComponentsToDestroy.Clear();
    }

    public virtual void OnCollision(Entity collision)
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].OnCollision(collision);
        }
    }


    public void Destroy(Entity entity)
    {
        world.Destroy(entity);
    }

    public void Destroy(Component component)
    {
        pendingComponentsToDestroy.Add(component);
    }

    public T GetComponent<T>() where T : Component
    {
        return components.Find(x => x.GetType().Name == typeof(T).Name) as T;
    }

    public T[] GetComponents<T>() where T : Component
    {
        return components.FindAll(x => x.GetType().Name == typeof(T).Name) as T[];
    }

    public T AddComponent<T>() where T : Component
    {
        T component = Activator.CreateInstance<T>();

        component.OnInstantiate(this);
        newComponents.Add(component);
        return component;
    }

    //public void AddComponent(Component component)
    //{
    //    component.OnInstantiate(this);
    //    newComponents.Add(component);
    //}

    public Entity Instantiate(Entity entity)
        => world.Instantiate(entity);

    public Entity FindEntityByTag(string tag)
    {
        return world.FindEntitiyByTag(tag);
    }

    public Entity[] FindEntitiesByTag(string tag)
    {
        return world.FindEntitiesByTag(tag);
    }
}
