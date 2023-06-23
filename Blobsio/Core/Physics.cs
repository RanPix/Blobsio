using Blobsio.Core.Entities;
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

                if (!objects[i].processCollision)
                    continue;
                
                if (CheckCollision(objects[i].collider, objects[i].position, objects[j].collider, objects[j].position))
                {
                    objects[i].OnCollision(objects[j]);
                }
            }
        }
    }

    private bool CheckCollision(float coll1, Vector2f coll1Pos, float coll2, Vector2f coll2Pos)
    {
        float minDistance = coll1 + coll2;
        float minDistance2 = minDistance * minDistance;

        float xSeparation = coll1Pos.X - coll2Pos.X;
        float ySeparation = coll1Pos.Y - coll2Pos.Y;

        float separation2 = (xSeparation * xSeparation) + (ySeparation * ySeparation);

        if (separation2 < minDistance2)
            return true;

        return false;
    }
}
