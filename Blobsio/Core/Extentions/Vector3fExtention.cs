using SFML.System;

namespace SFML_Thing.Core.Extentions;

public static class Vector3fExtention
{
    public static Vector2f Normalize(this Vector2f v)
    {
        float magnitude = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);

        return new Vector2f(v.X / magnitude, v.Y / magnitude); 
    }
}
