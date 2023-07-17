using Blobsio.Core.Entities;

namespace Blobsio.Core;

public class Scene
{
    internal static Engine engine;

    public static void LoadScene(Scene s)
    {
        engine.LoadScene(s.GetScene());
    }

    public virtual List<Entity> GetScene()
    {
        return new List<Entity>();
    }
}
