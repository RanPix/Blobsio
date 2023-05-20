namespace Blobsio.Core;

public static class Rand
{
    public static Random rand = new Random();

    public static float Next(float a, float b)
    {
        rand = new Random();

        return rand.Next((int)a, (int) b);
    }
}
