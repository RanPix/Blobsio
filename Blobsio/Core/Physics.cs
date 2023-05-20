using SFML.System;

namespace Blobsio.Core;

public class Physics
{ // there is no physics now but for now there will be a cycle xdxdxdd
    public void Update(Entity[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            for (int j = 0; j < objects.Length; j++)
            {
                if (i == j)
                    continue;

                if (CheckCollision(objects[i].collider, objects[i].position, objects[j].collider, objects[j].position))
                {
                    objects[i].OnCollisionEnter(objects[j]);
                }
            }
        }
    }

    private bool CheckCollision(Vector2f coll1, Vector2f coll1Pos, Vector2f coll2, Vector2f coll2Pos)
    {
        Vector2f coll1TopLeft = coll1Pos - coll1;
        Vector2f coll1BottomRight = coll1Pos + coll1;

        Vector2f coll2TopLeft = coll2Pos - coll2;
        Vector2f coll2BottomRight = coll2Pos + coll2;

        if (coll1TopLeft.X < coll2BottomRight.X &&
            coll1BottomRight.X > coll2TopLeft.X &&

            coll1TopLeft.Y < coll2BottomRight.Y &&
            coll1BottomRight.Y > coll2TopLeft.Y)
            return true;

        return false;
    }
}
