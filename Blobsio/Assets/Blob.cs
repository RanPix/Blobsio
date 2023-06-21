using Blobsio.Core;
using SFML.System;
using SFML.Graphics;
using Blobsio.Recources;

namespace Blobsio.Assets;

public class Blob : Component
{
    public View camera;

    public int size = 20;
    public float speed = 700;

    public (string name, Vector2i size, float frameTime, int framesCount)[] animations = 
        { ("blob", new Vector2i(16, 16), 0.1f, 4), ("rain", new Vector2i(16, 16), 0.07f, 4), ("glavie", new Vector2i(16, 16), 0.1f, 8) };

    public override void Start()
    {
        base.Start();

        camera = Renderer.mainCamera;

        entity.tag = "Blob";


        CircleShape shape = new CircleShape(size, 200);
        shape.OutlineThickness = 1f;
        entity.graphic = shape;
        entity.collider = size * 0.8f;
        entity.position = entity.position;
        entity.graphic.Origin = new Vector2f(size, size);

        int animIndex = Rand.rand.Next(animations.Length);
        GetComponent<Animation>().Setup(RecourcesManager.GetAnimationTexture(animations[animIndex].name), animations[animIndex].size, animations[animIndex].frameTime, animations[animIndex].framesCount);

        Respawn();
    }

    public override void OnCollision(Entity collision)
    {
        base.OnCollision(collision);

        if (collision.tag == "Blob")
        {
            Blob enemy = collision.GetComponent<Blob>();

            if (enemy.size > size)
                Respawn();

            else
                AddSize(enemy.size);
        }

        if (collision.tag == "Food")
        {
            Food point = collision.GetComponent<Food>();
            point.Respawn();

            AddSize(point.size);
        }

        if (collision.tag == "Spike" && size > 200)
        {
            Spike spike = collision.GetComponent<Spike>();

            spike.Respawn();
            AddSize(-size / 2);
        }
    }

    public Vector2f ConstrainToBounds(Vector2f velocity)
    {
        Vector2f newVelocity = new Vector2f();

        if (entity.position.X - size > 0f && velocity.X < 0f)
            newVelocity.X += velocity.X;
        if (entity.position.X + size < Game.MAP_SIZE && velocity.X > 0f)
            newVelocity.X += velocity.X;

        if (entity.position.Y - size > 0f && velocity.Y < 0f)
            newVelocity.Y += velocity.Y;
        if (entity.position.Y + size < Game.MAP_SIZE && velocity.Y > 0f)
            newVelocity.Y += velocity.Y;

        return newVelocity;
    }

    public void ThrowFood(Vector2f direction)
    {
        if (size < 100)
            return;

        Food food = Instantiate(new Entity(new List<Component>() { new Food() })).GetComponent<Food>();
        food.size = (int)(size * 0.05f);
        food.direction = direction;
        food.entity.position = entity.position + direction * size;

        size = (int)(size * 0.95f);
    }

    private void AddSize(int amount)
    {
        size = Math.Clamp(size + amount, 20, 10000000);

        CircleShape currentGraphic = (CircleShape)entity.graphic;
        currentGraphic.Radius = size;
        entity.graphic = currentGraphic;
        entity.graphic.Origin = new Vector2f(size, size);

        entity.collider = size * 0.8f;

        //cameraFOV = 1 + size / 1000 * 0.2f;
        //Console.WriteLine(cameraFOV);
        //Renderer.mainCamera.Zoom(cameraFOV);
    }

    private void Respawn()
    {
        size = 100;
        AddSize(0);

        int animIndex = Rand.rand.Next(animations.Length);
        GetComponent<Animation>().Setup(RecourcesManager.GetAnimationTexture(animations[animIndex].name), animations[animIndex].size, animations[animIndex].frameTime, animations[animIndex].framesCount);

        entity.position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }
}
