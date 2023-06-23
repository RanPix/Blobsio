using Blobsio.Core;
using SFML.Window;
using SFML.System;
using Blobsio.Core.Inputs;
using Blobsio.Core.Entities;

namespace Blobsio.Assets.Controllers;

public class BlobPlayerController : Component
{
    private Blob controlledBlob;
    private Vector2f moveDirection;

    public override void Start()
    {
        base.Start();

        entity.tag += "Player";

        Input.MovementInput += Move;
        Input.Create(ThrowFood, Keyboard.Key.F);
        Input.Create(DivideBlob, Keyboard.Key.Space);
        //Input.FoodThrowInput += ThrowFood;

        controlledBlob = (Blob)entity;
        controlledBlob.camera = Renderer.mainCamera;
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

    public override void OnDestroy()
    {
        base.OnDestroy();

        entity.tag -= "Player";
        controlledBlob.camera = null;
    }
}