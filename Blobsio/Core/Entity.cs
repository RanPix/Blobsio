using SFML.Graphics;
using SFML.System;

namespace Blobsio.Core;

public class Entity
{
    internal Game world { get; private set; }

    public string tag = "";
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

    private List<Component> components = new List<Component>();
    private List<Component> newComponents = new List<Component>();
    private List<Component> pendingComponentsToDestroy = new List<Component>();

    public Entity(List<Component> components)
    {
        this.components = components;
    }

    public void OnInstantiate(Game g)
    {
        world = g;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].OnInstantiate(this);
            components[i].Start();
        }
    }

    internal void Start()
    {
        if (newComponents.Count == 0)
            return;

        for (int i = 0; i < newComponents.Count; i++)
        {
            newComponents[i].Start();
        }

        components.AddRange(newComponents);
        newComponents.Clear();
    }

    internal void Update()
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].Update();
        }
    }

    internal void LateUpdate()
    {
        for (int i = 0; i < components.Count; i++)
        {
            components[i].LateUpdate();
        }
    }

    internal void Destroy()
    {
        if (pendingComponentsToDestroy.Count == 0)
            return;

        for (int i = 0; i < pendingComponentsToDestroy.Count; i++)
        {
            components.Remove(pendingComponentsToDestroy[i]);
        }

        pendingComponentsToDestroy.Clear();
    }

    //internal void OnDestroy() 
    //{
    //    for (int i = 0; i < components.Count; i++)
    //    {
    //        components[i].OnDestroy();
    //    }
    //}

    internal void OnCollision(Entity collision)
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
        T component = Activator.CreateInstance<T>();

        return components.Find(x => x.GetType().Name == component.GetType().Name) as T;
    }

    public List<T> GetComponents<T>() where T : Component
    {
        T component = Activator.CreateInstance<T>();

        return components.FindAll(x => x.GetType().Name == component.GetType().Name) as List<T>;
    }

    public T AddComponent<T>() where T : Component
    {
        T component = Activator.CreateInstance<T>();

        //component.OnInstantiate(this);
        newComponents.Add(component);
        return component;
    }

    //public void AddComponent(Component component)
    //{
    //    newComponents.Add(component);
    //}
}
