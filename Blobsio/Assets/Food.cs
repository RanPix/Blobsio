using Blobsio.Core;
using SFML.System;
using SFML.Graphics;
using Blobsio.Recources;
using SFML.Audio;
using Blobsio.Core.Entities;

namespace Blobsio.Assets;

public class Food : Entity
{
    public int size = 1;
    public bool canRespawn = true;

    public Vector2f direction;
    public float speed = 1000;

    public Food() { }
    public Food(List<Component> components) : base(components) { }

    public override void Start()
    {
        base.Start();

        tag += "Food";

        graphic = new CircleShape(size + 5, 10);
        graphic.Origin = new Vector2f(size + 5, size + 5);

        Animation anim = AddComponent<Animation>();
        anim.Setup(RecourcesManager.GetAnimationTexture("point"), 8, 0.4f, 2);

        collider = size + 5;
        processCollision = false;

        speed *= size * 0.2f;

        if (canRespawn)
            position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }

    public override void Update()
    {
        base.Update();

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
            Destroy(this);
    }
}
