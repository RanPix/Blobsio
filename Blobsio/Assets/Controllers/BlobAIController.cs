using Blobsio.Core;
using Blobsio.Core.Extentions;
using SFML.System;

namespace Blobsio.Assets.Controllers;

public class BlobAiController : BlobController
{
    private Blob blob;

    private float directionChangeTime = 2f; // це для примітивного ші
    private float directionChangeTimer = 2f;

    private Vector2f input;

    public override void Update()
    {
        directionChangeTimer += Time.deltaTime;

        MoveAI();
    }

    public override void SetBlob(Blob b)
    {
        blob = b;
    }

    private void MoveAI()
    {
        if (directionChangeTimer > directionChangeTime)
        {
            input = new Vector2f(Rand.Next(-100f, 100f), Rand.Next(-100f, 100f)).Normalize();
            directionChangeTimer = 0;
        }

        Vector2f velocity = input * blob.speed * Time.deltaTime;

        velocity = blob.ConstrainToBounds(velocity);

        blob.position += velocity;
    }
}