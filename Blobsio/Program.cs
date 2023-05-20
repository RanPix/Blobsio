using Blobsio.Assets;
using Blobsio.Core;
using SFML.System;

namespace Blobsio;

static class Program
{
    private static List<Entity> entities = new List<Entity>()
    {
        new Blob() { position = new Vector2f(300, 300) },
        new Blob() { position = new Vector2f(300, 500) },
    };

    static void Main()
    {
        Game game = new Game(entities);
        game.Run();
    }
}