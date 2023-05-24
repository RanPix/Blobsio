using Blobsio.Core;

namespace Blobsio.Assets;

public class PointSpawner : Entity
{
    private int maxPointsAmount = 2000;

    public override void Start()
    {
        base.Start();

        for (int i = 0; i < maxPointsAmount; i++)
        {
            world.Instantiate(new Point());
        }
    }
}