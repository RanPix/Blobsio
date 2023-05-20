using SFML.Graphics;
using SFML.System;

namespace Blobsio.Core;

public abstract class Entity
{
    public Tag tag = Tag.None;
    public Transformable graphic = new CircleShape(10f, 3);

    public Vector2f collider;

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

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void OnDestroy() 
    {
    
    }

    public virtual void OnCollisionEnter(Entity collision)
    {

    }
}
