using Blobsio.Core;
using SFML.System;
using SFML.Graphics;
using System.Drawing;

namespace Blobsio.Assets;

public class Food : Entity
{
    public int size = 1;
    private bool canRespawn = true;

    private Vector2f direction;
    private float speed = 1000;

    public Food(int size, bool canRespawn, Vector2f direction = new Vector2f(), Vector2f position = new Vector2f())
    {
        this.size = size;
        this.canRespawn = canRespawn;
        this.direction = direction;
        this.position = position;
    }

    public override void Start()
    {
        base.Start();

        graphic = new CircleShape(size + 5, 10);
        graphic.Origin = new Vector2f(size + 5, size + 5);

        collider = size + 5;
        processCollision = false;

        speed *= size * 0.2f;

        tag = Tag.Point;

        if (canRespawn)
            position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }

    public override void Update()
    {
        if (canRespawn || speed < 0)
            return;

        position += direction * speed * Time.deltaTime;

        speed -= Time.deltaTime * 2000;
    }

    public void Respawn()
    {
        if (canRespawn)
            position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));

        else // мені було ліньки думати
            world.Destroy(this);
    }
}
