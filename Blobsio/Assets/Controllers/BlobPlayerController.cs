using Blobsio.Core;
using SFML.Window;
using SFML.System;

namespace Blobsio.Assets.Controllers;

public class BlobPlayerController : Component
{
    private Blob controlledBlob;
    private Vector2f moveDirection;

    public override void Start()
    {
        Input.MovementInput += Move;
        Input.Create(ThrowFood, Keyboard.Key.F);
        Input.Create(DivideBlob, Keyboard.Key.Space);
        //Input.FoodThrowInput += ThrowFood;

        controlledBlob = GetComponent<Blob>();
    }

    private void Move(Vector2f input)
    {
        moveDirection = input;

        Vector2f velocity = input * controlledBlob.speed * Time.deltaTime;

        velocity = controlledBlob.ConstrainToBounds(velocity);

        entity.position += velocity;
        Renderer.mainCamera.Center = entity.position;
    }

    private void ThrowFood()
    {
        controlledBlob.ThrowFood(moveDirection);
    }

    private void DivideBlob()
    {

    }
}