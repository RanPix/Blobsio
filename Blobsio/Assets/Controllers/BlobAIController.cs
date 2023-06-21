using Blobsio.Core;
using Blobsio.Core.Extentions;
using SFML.System;

namespace Blobsio.Assets.Controllers;

public class BlobAiController : Component
{
    private Blob blob;

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
        blob = GetComponent<Blob>();
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

        entity.position += velocity;
    }
}