using Blobsio.Core.Entities;
using SFML.Graphics;
using SFML.System;

namespace Blobsio.Assets;

public class Animation : Component
{
    private Texture texture;

    private int frameSize = 16;
    private float frameTime = 0.1f;
    private float frameTimer;
    private int frameCount = 4;
    private int currentFrame = 1;

    public override void Update()
    {
        base.Update();

        UpdateAnimation();
    }

    public void Setup(Texture texture, int frameSize, float frameTime, int frameCount)
    {
        this.texture = texture;
        CircleShape shape = (CircleShape)entity.graphic;
        shape.Texture = texture;
        this.frameSize = frameSize;
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
        currentGraphic.TextureRect = new IntRect(new Vector2i((frameSize * currentFrame) - frameSize, 0), new Vector2i(frameSize, frameSize));

        currentFrame++;
        if (currentFrame > frameCount)
            currentFrame = 1;
    }
}
