using Blobsio.Core.Entities;
using SFML.System;

namespace Blobsio.Core;

public class Physics
{
    public void Update(List<Entity> entities)
    {
        //List<(Entity, Entity)> collisions = DetectPossibleCollisions(objects);

        //foreach ((Entity, Entity) collision in collisions)
        //{
        //    //if (!objects[i].processCollision)
        //    //    continue;

        //    if (CheckCollision(collision.Item1.collider, collision.Item1.position, collision.Item2.collider, collision.Item2.position))
        //    {
        //        collision.Item1.OnCollision(collision.Item2);
        //        collision.Item2.OnCollision(collision.Item1);
        //    }
        //}

        ProcessCollisionNew(entities);

        //for (int i = 0; i < entities.Length; i++)
        //{
        //    if (!entities[i].processCollision)
        //        continue;

        //    for (int j = 0; j < entities.Length; j++)
        //    {
        //        if (i == j)
        //            continue;

        //        if (CheckCollision(entities[i].collider, entities[i].position, entities[j].collider, entities[j].position))
        //        {
        //            entities[i].OnCollision(entities[j]);
        //        }
        //    }
        //}
    }

    private void ProcessCollisionNew(List<Entity> entities)
    {
        //List<Collider> colliders = GetEntityColliders(entities);

        //for (int i = 0; i < colliders.Count; i++)
        //{
        //    for (int j = 0; j < colliders.Count; j++)
        //    {
        //        if (i == j)
        //            continue;

        //        if (!colliders[i].processCollision)
        //            continue;

        //        if (CheckCollision(colliders[i].radius, colliders[i].entity.position, colliders[j].radius, colliders[j].entity.position))
        //        {
        //            colliders[i].entity.OnCollision(colliders[j].entity);
        //        }
        //    }
        //}

        Collider coll1 = null;
        Collider coll2 = null;

        for (int i = 0; i < entities.Count; i++)
        {
            if (!entities[i].processCollision)
                continue;

            coll1 = entities[i].GetComponent<Collider>();

            if (coll1 == null)
                continue;

            for (int j = 0; j < entities.Count; j++)
            {
                if (i == j)
                    continue;

                coll2 = entities[j].GetComponent<Collider>();

                if (coll2 == null)
                    continue;

                if (CheckCollision(coll1.radius, entities[i].position, coll2.radius, entities[j].position))
                {
                    entities[i].OnCollision(entities[j]);
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

        //Console.WriteLine(separation2 < minDistance2);

        return separation2 < minDistance2;
    }

    //private List<(Entity, Entity)> DetectPossibleCollisions(Entity[] objects) // Sweep and Prune method
    //{
    //    Entity[] colliders = new Entity[objects.Length];

    //    for (int i = 0; i < objects.Length; i++)
    //        colliders[i] = objects[i];

    //    Array.Sort(colliders);
    //    //SortCollidersAlongAxis(colliders);

    //    List<(Entity, Entity)> possiblePairs = CheckForPossiblePairsIL(colliders);

    //    return possiblePairs;
    //}

    //private List<(Entity, Entity)> CheckForPossiblePairsIL(IEnumerable<Entity> entities)
    //{
    //    List<(Entity, Entity)> possiblePairs = new();
    //    List<Entity> currentPool = new();
    //    Queue<Entity> deletionQueue = new();

    //    foreach (var _entity in entities)
    //    {
    //        for (int i = 0; i < currentPool.Count; i++)
    //        {
    //            if (BoundingAxisCollision(_entity, currentPool[i]))
    //                possiblePairs.Add((currentPool[i], _entity));
    //            else
    //                deletionQueue.Enqueue(currentPool[i]);
    //        }

    //        while (deletionQueue.Count > 0)
    //        {
    //            if (deletionQueue.Count <= 0) 
    //                break;

    //            Entity index = deletionQueue.Dequeue();
    //            currentPool.Remove(index);
    //        }

    //        currentPool.Add(_entity);
    //    }

    //    return possiblePairs;
    //}

    //private List<(Entity, Entity)> CheckForPossiblePairsIL(Entity[] entities)
    //{
    //    List<(Entity, Entity)> possiblePairs = new();
    //    int poolCount = 0;

    //    for (int i = 0; i < entities.Length; i++)
    //    {
    //        for (int j = poolCount; j < i; j++)
    //        {
    //            if (BoundingAxisCollision(entities[i], entities[j]))
    //                possiblePairs.Add((entities[i], entities[j]));
    //            else
    //                poolCount++;
    //        }
    //    }

    //    return possiblePairs;
    //}

    private bool BoundingAxisCollision(float pos1, float size1, float pos2, float size2)
    {
        float coll1Left = pos1 - size1;
        float coll1Right = pos1 + size1;

        float coll2Left = pos2 - size2;
        float coll2Right = pos2 + size2;

        return (coll1Left > coll2Left && coll2Left < coll1Right) || (coll1Left < coll2Right && coll2Right > coll1Right);
    }

    //private bool BoundingAxisCollision(Entity col1, Entity col2)
    //    => BoundingAxisCollision(col1.position.X, col1.collider, col2.position.X, col2.collider);

    private void SortCollidersAlongAxis(Entity[] colliders)
    {
        QuickSort(colliders, 0, colliders.Length - 1);
    }

    public static void QuickSort(Entity[] A, int left, int right)
    {
        if (left > right || left < 0 || right < 0) 
            return;

        int index = Partition(A, left, right);

        if (index != -1)
        {
            QuickSort(A, left, index - 1);
            QuickSort(A, index + 1, right);
        }
    }

    private static int Partition(Entity[] A, int left, int right)
    {
        if (left > right) 
            return -1;

        int end = left;

        float pivot = A[right].position.X;    // choose last one to pivot, easy to code
        for (int i = left; i < right; i++)
        {
            if (A[i].position.X < pivot)
            {
                Swap(A, i, end);
                end++;
            }
        }

        Swap(A, end, right);

        return end;
    }

    private static void Swap(Entity[] A, int left, int right)
    {
        Entity tmp = A[left];
        A[left] = A[right];
        A[right] = tmp;
    }
}
