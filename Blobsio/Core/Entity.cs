using SFML.Graphics;
using SFML.System;

namespace Blobsio.Core;

public abstract class Entity
{
    protected Game world;

    public Tag tag = Tag.None;
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

    public void OnInstantiate(Game g)
    {
        world = g;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void OnDestroy() 
    {
    
    }

    public virtual void OnCollision(Entity collision)
    {

    }
}
