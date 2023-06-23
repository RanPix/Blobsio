using Blobsio.Core;
using Blobsio.Core.Entities;
using Blobsio.Recources;
using SFML.Graphics;
using SFML.System;

namespace Blobsio.Assets;

public class Spike : Entity
{
    private float radius = 70;

    public Spike(List<Component> components) : base(components) { }

    public override void Start()
    {
        base.Start();

        tag += "Spike";
        processCollision = false;

        CircleShape shape = new CircleShape(radius, 30);
        //shape.FillColor = new Color(100, 100, 100);

        graphic = shape;
        collider = radius;
        graphic.Origin = new Vector2f(radius, radius);

        collider = radius;
        position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));


        Animation anim = GetComponent<Animation>();
        anim.Setup(RecourcesManager.GetAnimationTexture("spike"), 16, 0.2f, 2);
    }

    public void Respawn()
    {
        position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }
}
