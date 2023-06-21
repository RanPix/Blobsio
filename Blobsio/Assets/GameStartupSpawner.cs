using Blobsio.Assets.Controllers;
using Blobsio.Core;

namespace Blobsio.Assets;

public class GameStartupSpawner : Component
{
    private int pointsAmount = 1000;
    private int playersAmount = 10;
    private int spikesAmount = 25;

    public override void Start()
    {
        base.Start();

        Instantiate(new Entity(new List<Component>() { new Blob(), new BlobPlayerController() }));

        for (int i = 0; i < playersAmount; i++)
        {
            Instantiate(new Entity(new List<Component>() { new Blob(), new BlobAiController() }));
        }

        for (int i = 0; i < spikesAmount; i++)
        {
            Instantiate(new Entity(new List<Component>() { new Spike() }));
        }

        for (int i = 0; i < pointsAmount; i++)
        {
            Instantiate(new Entity(new List<Component>() { new Food() }));
        }

        Destroy(entity);
    }
}