using Blobsio.Core;
using Blobsio.Recources;
using SFML.Graphics;
using SFML.System;

namespace Blobsio.Assets;

public class Spike : Component
{
    private float radius = 70;

    public override void Start()
    {
        base.Start();

        entity.tag = "Spike";
        entity.processCollision = false;

        CircleShape shape = new CircleShape(radius, 30);
        //shape.FillColor = new Color(100, 100, 100);

        entity.graphic = shape;
        entity.collider = radius;
        entity.graphic.Origin = new Vector2f(radius, radius);

        entity.collider = radius;
        entity.position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));


        Animation anim = AddComponent<Animation>();
        anim.Setup(RecourcesManager.GetAnimationTexture("spike"), new Vector2i(16, 16), 0.2f, 2);
    }

    public void Respawn()
    {
        entity.position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }
}
