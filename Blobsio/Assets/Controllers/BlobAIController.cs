using Blobsio.Core;
using Blobsio.Core.Entities;
using Blobsio.Core.Extentions;
using SFML.System;

namespace Blobsio.Assets.Controllers;

public class BlobAiController : Component
{
    private Blob controlledBlob;

    private float directionChangeTime = 2f;
    private float directionChangeTimer = 2f;

    private Vector2f input;

    public override void Update()
    {
        directionChangeTimer += Time.deltaTime;

        MoveAI();
    }

    public override void Start()
    {
        controlledBlob = (Blob)entity;
    }

    private void MoveAI()
    {
        if (directionChangeTimer > directionChangeTime)
        {
            input = new Vector2f(Rand.Next(-100f, 100f), Rand.Next(-100f, 100f)).Normalize();
            directionChangeTimer = 0;
        }

        Vector2f velocity = input * controlledBlob.speed * Time.deltaTime;

        velocity = controlledBlob.ConstrainToBounds(velocity);

        entity.position += velocity;
    }
}