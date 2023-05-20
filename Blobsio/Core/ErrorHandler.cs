namespace SFML_Thing.Core;

public static class ErrorHandler
{
    public static void Error(Exception e)
    {
#if DEBUG
        throw e;
#endif
    }
}
