using Blobsio.Core;

namespace Blobsio.Recources;

public static class RecourcesManager
{
    public static string GetFont(string name)
    {
        string path = "";

        try
        {
#if DEBUG
            path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Recources\Fonts\" + name;
#else
            path = Directory.GetCurrentDirectory() + @"\Recources\Fonts\" + name;
#endif
        }
        catch
        {
            ErrorHandler.Error(new Exception("The path or the name of the font is incorrect."));
        }

        return path;
    }
}
