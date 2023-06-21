using Blobsio.Assets;

namespace Blobsio.Core;

static class Program
{
    private static List<Entity> entities = new List<Entity>()
    {
        new Entity(new List<Component> { new GameStartupSpawner() }),
    };

    static void Main()
    {
        Game.Create(entities).Run();
    }
}