using Blobsio.Core;
using Blobsio.Core.Entities;
using Blobsio.Assets;

namespace Agari.Assets.Scenes;

public class MainScene : Scene
{
    public List<Entity> scene = new()
    {
        new StartupSpawner(),
    };

    public override List<Entity> GetScene()
    {
        return scene;
    }
}
