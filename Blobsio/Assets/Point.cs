using Blobsio.Core;
using Blobsio.Core.Interfaces;
using SFML.System;
using SFML.Graphics;

namespace Blobsio.Assets;

public class Point : Entity, IDrawable
{
    public override void Start()
    {
        base.Start();

        graphic = new CircleShape(5, 10);
        graphic.Origin = new Vector2f(5, 5);

        collider = 5;
        processCollision = false;

        tag = Tag.Point;

        Respawn();
    }

    public void Respawn()
        => position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));


    public Drawable Draw()
        => (Drawable)graphic;
}
