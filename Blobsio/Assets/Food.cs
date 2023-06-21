using Blobsio.Core;
using SFML.System;
using SFML.Graphics;
using System.Drawing;

namespace Blobsio.Assets;

public class Food : Component
{
    public int size = 1;
    public bool canRespawn = true;

    public Vector2f direction;
    public float speed = 1000;

    public override void Start()
    {
        base.Start();

        entity.graphic = new CircleShape(size + 5, 10);
        entity.graphic.Origin = new Vector2f(size + 5, size + 5);

        entity.collider = size + 5;
        entity.processCollision = false;

        speed *= size * 0.2f;

        entity.tag = "Food";

        if (canRespawn)
            entity.position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }

    public override void Update()
    {
        if (canRespawn || speed < 0)
            return;

        entity.position += direction * speed * Time.deltaTime;

        speed -= Time.deltaTime * 2000;
    }

    public void Respawn()
    {
        if (canRespawn)
            entity.position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));

        else // мені було ліньки думати
            Destroy(entity);
    }
}
