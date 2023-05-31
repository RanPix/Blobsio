using Blobsio.Core;
using SFML.System;
using SFML.Graphics;
using Blobsio.Assets.Controllers;

namespace Blobsio.Assets;

public class Blob : Entity
{
    public BlobController controller;
    public View camera;

    public int size = 20;
    public float speed = 700;

    public Blob (BlobController controller, View camera = null)
    {
        this.controller = controller;
        this.controller.SetBlob(this);
        this.camera = camera;
    }

    public override void Start()
    {
        base.Start();

        tag = Tag.Blob;

        CircleShape shape = new CircleShape(size, 30);
        graphic = shape;

        collider = size * 0.8f;
        position = position;
        graphic.Origin = new Vector2f(size, size);

        Respawn();
    }

    public override void Update()
    {
        if (controller != null)
            controller.Update();
    }

    public override void OnCollision(Entity collision)
    {
        base.OnCollision(collision);

        if (collision.tag == Tag.Blob)
        {
            Blob enemy = (Blob)collision;

            if (enemy.size > size)
                Respawn();

            else
                AddSize(enemy.size);
        }

        if (collision.tag == Tag.Point)
        {
            Food point = (Food)collision;
            point.Respawn();

            AddSize(point.size);
        }

        if (collision.tag == Tag.Spike && size > 200)
        {
            Spike spike = (Spike)collision;

            spike.Respawn();
            AddSize(-size / 2);
        }
    }

    public Vector2f ConstrainToBounds(Vector2f velocity)
    {
        Vector2f newVelocity = new Vector2f();

        if (position.X - size > 0f && velocity.X < 0f)
            newVelocity.X += velocity.X;
        if (position.X + size < Game.MAP_SIZE && velocity.X > 0f)
            newVelocity.X += velocity.X;

        if (position.Y - size > 0f && velocity.Y < 0f)
            newVelocity.Y += velocity.Y;
        if (position.Y + size < Game.MAP_SIZE && velocity.Y > 0f)
            newVelocity.Y += velocity.Y;

        return newVelocity;
    }

    private void SwapBlobs(bool b)
    {
        if (!b)
            return;

    public void ThrowFood(Vector2f direction)
    {
        if (size < 100)
            return;

        world.Instantiate(new Food((int)(size * 0.05f), false, direction, position + direction * size));
        size = (int)(size * 0.95f);
    }

    private void AddSize(int amount)
    {
        size = Math.Clamp(size + amount, 20, 10000000);

        CircleShape currentGraphic = (CircleShape)graphic;
        currentGraphic.Radius = size;
        graphic = currentGraphic;
        graphic.Origin = new Vector2f(size, size);

        collider = size * 0.8f;

        //cameraFOV = 1 + size / 1000 * 0.2f;
        //Console.WriteLine(cameraFOV);
        //Renderer.mainCamera.Zoom(cameraFOV);
    }

    private void Respawn()
    {
        size = 100;
        AddSize(0);

        position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }

    public Drawable Draw()
        => (Drawable)graphic;
}
