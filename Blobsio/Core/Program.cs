using Blobsio.Assets;
using Blobsio.Core.Entities;

namespace Blobsio.Core;

static class Program
{
    private static List<Entity> entities = new List<Entity>()
    {
        new StartupSpawner(),
    };

    static void Main()
    {
        Game.Create(entities).Run();
    }
}