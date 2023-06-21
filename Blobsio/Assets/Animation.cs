using Blobsio.Core;
using SFML.Graphics;
using SFML.System;

namespace Blobsio.Assets;

public class Animation : Component
{
    private Texture texture;

    private Vector2i animationFrameSize = new Vector2i(16, 16);
    private float frameTime = 0.1f;
    private float frameTimer;
    private int frameCount = 4;
    private int currentFrame = 1;

    public override void Update()
    {
        base.Update();

        UpdateAnimation();
    }

    public void Setup(Texture texture, Vector2i animationFrameSize, float frameTime, int frameCount)
    {
        CircleShape shape = (CircleShape)entity.graphic;
        shape.Texture = texture;
        this.animationFrameSize = animationFrameSize;
        this.frameTime = frameTime;
        this.frameCount = frameCount;
    }

    private void UpdateAnimation()
    {
        frameTimer += Time.deltaTime;
        if (frameTimer < frameTime)
            return;
        frameTimer = 0;

        CircleShape currentGraphic = (CircleShape)entity.graphic;
        currentGraphic.TextureRect = new IntRect(new Vector2i((animationFrameSize.X * currentFrame) - animationFrameSize.X, 0), animationFrameSize);

        currentFrame++;
        if (currentFrame > frameCount)
            currentFrame = 1;
    }
}
