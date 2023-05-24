using Blobsio.Assets;
using Blobsio.Core;

namespace Blobsio;

static class Program
{
    private static List<Entity> entities = new List<Entity>()
    {
        new PointSpawner(),

        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),
        new Spike(),


        new Blob(),
        new Blob() { isAI = true },
        new Blob() { isAI = true },
        new Blob() { isAI = true },
        new Blob() { isAI = true },
        new Blob() { isAI = true },
        new Blob() { isAI = true },
        new Blob() { isAI = true },
        new Blob() { isAI = true },
    };

    static void Main()
    {
        Game game = new Game(entities);
        game.Run();
    }
}