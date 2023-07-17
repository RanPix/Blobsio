global using Time = Blobsio.Core.Time;
using Agari.Assets.Scenes;

namespace Blobsio.Core;

static class Program
{
    static void Main()
    {
        Engine.Create(new MainScene()).Run();
    }
}