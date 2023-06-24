using Blobsio.Core;
using SFML.System;
using SFML.Graphics;
using Blobsio.Recources;
using SFML.Audio;
using Blobsio.Core.Entities;

namespace Blobsio.Assets;

public class Blob : Entity
{
    public View camera;

    private int size = 20;
    public float speed = 700;

    public (string name, int size, float frameTime, int framesCount)[] animations = 
        { ("blob", 16, 0.1f, 4), ("rain", 16, 0.07f, 4), ("glavie", 16, 0.1f, 8) };

    public Blob(List<Component> components) : base(components) { }

    public override void Start()
    {
        base.Start();

        camera = Renderer.mainCamera;

        tag += "Blob";

        CircleShape shape = new CircleShape(size, 200);
        shape.OutlineThickness = 1f;
        graphic = shape;
        collider = size * 0.8f;
        position = position;
        graphic.Origin = new Vector2f(size, size);

        int animIndex = Rand.rand.Next(animations.Length);

        GetComponent<Animation>().Setup(RecourcesManager.GetAnimationTexture(animations[animIndex].name), animations[animIndex].size, animations[animIndex].frameTime, animations[animIndex].framesCount);

        Respawn();
    }

    public override void OnCollision(Entity collision)
    {
        base.OnCollision(collision);

        if (collision.tag == "Blob")
        {
            Blob enemy = (Blob)collision;

            if (enemy.size > size)
                Respawn();

            else
                AddSize(enemy.size);
        }

        if (collision.tag == "Food")
        {
            if (tag == "Player")
            {
                AudioSystem.PlaySoundOnce("pop", GetComponent<AudioSource>().clip);
                //Sound sound = new Sound(RecourcesManager.GetSoundBuffer("pop2"));
                //sound.Play();
            }

            Food point = (Food)collision;
            point.Respawn();

            AddSize(point.size);
        }

        if (collision.tag == "Spike" && size > 200)
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

    public void ThrowFood(Vector2f direction)
    {
        if (size < 100)
            return;

        Food food = (Food)Instantiate(new Food());
        food.size = (int)(size * 0.05f);
        food.direction = direction;
        food.position = position + direction * size;

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
        size = 20;
        AddSize(0);

        int animIndex = Rand.rand.Next(animations.Length);
        GetComponent<Animation>().Setup(RecourcesManager.GetAnimationTexture(animations[animIndex].name), animations[animIndex].size, animations[animIndex].frameTime, animations[animIndex].framesCount);

        position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
    }
}
