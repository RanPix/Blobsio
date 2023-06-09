using SFML.Graphics;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace Blobsio.Recources;

public static class RecourcesManager
{
    private static Assembly assembly = Assembly.GetExecutingAssembly();

    public static Font GetFont(string name)
    {
        using (Stream stream = assembly.GetManifestResourceStream("Blobsio.Recources.Fonts" + name))
        {
            return new Font(stream);
        }
    }


    public static Stream GetDefaultConfig(string name)
        => assembly.GetManifestResourceStream("Blobsio.Recources.Configs.defaultConfig.cfg");

    public static Stream GetConfig(string name)
        => assembly.GetManifestResourceStream($"Blobsio.Recources.Configs.{name}.cfg");
}
