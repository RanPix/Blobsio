using SFML.System;

namespace Blobsio.Core.Extentions;

public static class Vector2fExtention
{
    public static Vector2f Normalize(this Vector2f v)
    {
        if (v == new Vector2f(0, 0)) return v;

        float magnitude = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);

        return new Vector2f(v.X / magnitude, v.Y / magnitude); 
    }

    public static Vector2f Lerp(Vector2f start, Vector2f end, float time)
    {
        return start + (end - start) * time;
    }
}
