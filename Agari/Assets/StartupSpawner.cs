using Blobsio.Assets.Controllers;
using Blobsio.Core.Entities;

namespace Blobsio.Assets;

public class StartupSpawner : Entity
{
    private int pointsAmount = 1000;
    private int playersAmount = 10;
    private int spikesAmount = 25;

    public override void Start()
    {
        base.Start();

        //Instantiate(new AudioSystem(new List<AudioClipID>() { new AudioClipID(new AudioClip("pop2"), "pop") }));

        Instantiate(new Blob(new List<Component>() { new BlobPlayerController(), new Animation() }));//, new Animation(), new AudioSource("pop2"), new AudioSourceID("pop") }));

        for (int i = 0; i < playersAmount; i++)
        {
            Instantiate(new Blob(new List<Component>() { new BlobAiController(), new Animation() }));
        }

        for (int i = 0; i < spikesAmount; i++)
        {
            Instantiate(new Spike(new List<Component>() { new Animation() }));
        }

        for (int i = 0; i < pointsAmount; i++)
        {
            Instantiate(new Food(/*new List<Component>() { new Animation() }*/));
        }

        Destroy(this);
    }
}