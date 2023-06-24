using Blobsio.Core.Entities;
using SFML.System;

namespace Blobsio.Core;

public class Physics
{
    public void Update(Entity[] objects)
    {
        //List<List<int>> collisions = DetectPossibleCollisions(objects);

        //foreach (List<int> collision in collisions)
        //{
        //    //Console.WriteLine(collision.Count);
        //    for (int i = 0; i < collision.Count; i++)
        //    {
        //        //Console.Write(collision[i]);
        //        for (int j = 0; j < collision.Count; j++)
        //        {
        //            if (i == j)
        //                continue;

        //            if (!objects[i].processCollision)
        //                continue;

        //            if (CheckCollision(objects[i].collider, objects[i].position, objects[j].collider, objects[j].position))
        //            {
        //                objects[i].OnCollision(objects[j]);
        //            }

        //            Console.WriteLine();
        //        }
        //    }
        //}

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

    private List<List<int>> DetectPossibleCollisions(Entity[] objects) // Sweep and Prune method
    {
        List<List<int>> possibleCollisions = new List<List<int>>();
        List<int> active = new List<int>() { 0 };
        Entity[] colliders = new Entity[objects.Length];

        for (int i = 0; i < objects.Length; i++)
            colliders[i] = objects[i];

        SortCollidersAlongAxis(colliders);

        int lastActive = 0;
        for (int i = 1; i < colliders.Length; i++)
        {
            for (int j = 0; j < active.Count; j++)
            {
                if (j - 1 !> i)
                    break;

                if (BoundingAxisCollision(colliders[active[j]].position.X, colliders[active[j]].collider, colliders[i].position.X, colliders[i].collider))
                {

                    active.Add(i);
                    Console.WriteLine(true);
                    continue;
                }

                break;
            }

            lastActive = i;

            foreach (int activea in active)
                Console.WriteLine(activea);

            possibleCollisions.Add(active);
            active.Clear();
            active.Add(i);
        }

        return possibleCollisions;
    }

    private bool BoundingAxisCollision(float pos1, float size1, float pos2, float size2)
    {
        float coll1Left = pos1 - size1;
        float coll1Right = pos1 + size1;

        float coll2Left = pos2 - size2;
        float coll2Right = pos2 + size2;

        return coll1Left < coll2Right && coll1Right > coll2Left;
    }

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
