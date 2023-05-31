using Blobsio.Core;
using Blobsio.Core.Extentions;
using SFML.System;

namespace Blobsio.Assets.Controllers;

public class BlobPlayerController : BlobController
{
    private Blob controlledBlob;
    Vector2f moveDirection;

    public override void Update()
    {

    }

    public override void SetBlob(Blob b)
    {
        Input.MovementInput += Move;
        Input.FoodThrowInput += ThrowFood;

        controlledBlob = b;
    }

    private void Move(Vector2f input)
    {
        moveDirection = input;

        Vector2f velocity = input * controlledBlob.speed * Time.deltaTime;

        velocity = controlledBlob.ConstrainToBounds(velocity);

        controlledBlob.position += velocity;
        Renderer.mainCamera.Center = controlledBlob.position;
    }

    private void ThrowFood()
    {
        controlledBlob.ThrowFood(moveDirection);
    }
}