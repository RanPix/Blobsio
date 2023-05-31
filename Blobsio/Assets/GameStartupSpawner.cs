using Blobsio.Assets.Controllers;
using Blobsio.Core;

namespace Blobsio.Assets;

public class GameStartupSpawner : Entity
{
    private int pointsAmount = 1000;
    private int playersAmount = 10;
    private int spikesAmount = 25;

    public override void Start()
    {
        base.Start();

        world.Instantiate(new Blob(new BlobPlayerController()));

        for (int i = 0; i < playersAmount; i++)
        {
            world.Instantiate(new Blob(new BlobAiController()));
        }

        for (int i = 0; i < spikesAmount; i++)
        {
            world.Instantiate(new Spike());
        }

        for (int i = 0; i < pointsAmount; i++)
        {
            world.Instantiate(new Food(1, true));
        }

        world.Destroy(this);
    }
}