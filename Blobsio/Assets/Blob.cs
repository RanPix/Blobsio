﻿using Blobsio.Core;
using SFML.System;
using SFML.Graphics;
using Blobsio.Core.Extentions;
using Blobsio.Core.Interfaces;

namespace Blobsio.Assets;

public class Blob : Entity, IUpdatable, IDrawable
{
    public bool isAI;
    public int size = 20;
    public float speed = 700;

    private float cameraFOV = 1f;

    private float aiDirectionChangeTime = 2f; // це для примітивного ші
    private float aiDirectionChangeTimer = 2f;

    private Vector2f aiInput;

    public override void Start()
    {
        base.Start();

        tag = Tag.Blob;

        CircleShape shape = new CircleShape(size, 30);
        graphic = shape;

        collider = size * 0.8f;
        position = position;
        graphic.Origin = new Vector2f(size, size);

        Renderer.mainCamera.Zoom(cameraFOV);

        Respawn();

        if (!isAI)
            Input.MovementInput += MovePlayer;
    }

    public void Update()
    {
        aiDirectionChangeTimer += Time.deltaTime;

        MoveAI();
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
            Point point = (Point)collision;
            point.Respawn();

            AddSize(1);
        }

        if (collision.tag == Tag.Spike && size > 200)
        {
            Spike spike = (Spike)collision;

            spike.Respawn();
            AddSize(-size / 2);
        }
    }

    private void MovePlayer(Vector2f input)
    {
        Vector2f velocity = input * speed * Time.deltaTime;

        velocity = ConstrainToBounds(velocity);

        position += velocity;
        Renderer.mainCamera.Center = position;
    }

    private void MoveAI()
    {
        if (!isAI)
            return;

        if (aiDirectionChangeTimer > aiDirectionChangeTime)
        {
            aiInput = new Vector2f(Rand.Next(-100f, 100f), Rand.Next(-100f, 100f)).Normalize();
            aiDirectionChangeTimer = 0;
        }

        Vector2f velocity = aiInput * speed * Time.deltaTime;

        velocity = ConstrainToBounds(velocity);

        position += velocity;
    }

    private Vector2f ConstrainToBounds(Vector2f velocity)
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

        position = new Vector2f(Rand.Next(0, Game.MAP_SIZE), Rand.Next(0, Game.MAP_SIZE));
        

        if (!isAI)
        {
            Renderer.mainCamera.Center = position;
        }
    }
    public Drawable Draw()
        => (Drawable)graphic;
}