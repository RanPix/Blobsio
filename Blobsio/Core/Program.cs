using Blobsio.Assets;

namespace Blobsio.Core;

static class Program
{
    private static List<Entity> entities = new List<Entity>()
    {
        new GameStartupSpawner(),

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
    };

    static void Main()
    {
        Game.Create(entities).Run();
    }
}