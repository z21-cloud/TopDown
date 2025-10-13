using UnityEngine;
using System.Collections.Generic;

public static class GridSampling
{
    public static Vector3 GetValidSpawnPoint(
        Collider2D bossCollider,
        float radius,
        float minDistance,
        LayerMask spikeMask,
        float cellSize = -1)
    {
        if (cellSize <= 0) cellSize = minDistance;

        Vector3 bossPos = bossCollider.transform.position;

        List<Vector3> validPoints = BuildValidGrid(
            bossPos,
            radius,
            cellSize,
            bossCollider,
            minDistance,
            spikeMask
        );

        if(validPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, validPoints.Count);
            return validPoints[randomIndex];
        }

        return bossPos;
    }

    private static List<Vector3> BuildValidGrid(
        Vector3 center, 
        float radius, 
        float cellSize, 
        Collider2D bossCollider, 
        float minDistance, 
        LayerMask spikeMask)
    {
        List<Vector3> validPoints = new List<Vector3>();

        float minX = center.x - radius;
        float maxX = center.x + radius;
        float minY = center.y - radius;
        float maxY = center.y + radius;

        for (float x = minX; x <= maxX; x+= cellSize)
        {
            for (float y = minY; y <= maxY; y += cellSize)
            {
                Vector3 point = new Vector3(x, y, center.z);

                if (Vector3.Distance(point, center) > radius) continue;

                if (bossCollider.OverlapPoint(point)) continue;

                Collider2D nearby = Physics2D.OverlapCircle(point, minDistance, spikeMask);
                if (nearby != null) continue;

                validPoints.Add(point);
            }
        }

        return validPoints;
    }

    public static void DrawGrid(
        Vector3 center,
        float radius,
        float cellSize,
        Collider2D bossCollider,
        float minDistance,
        LayerMask spikeMask)
    {
        List<Vector3> validPoints = new List<Vector3>();

        float minX = center.x - radius;
        float maxX = center.x + radius;
        float minY = center.y - radius;
        float maxY = center.y + radius;

        for (float x = minX; x <= maxX; x += cellSize)
        {
            for (float y = minY; y <= maxY; y += cellSize)
            {
                Vector3 point = new Vector3(x, y, center.z);

                if (Vector3.Distance(point, center) > radius) continue;

                bool isValid = true;

                if (bossCollider.OverlapPoint(point)) isValid = false;

                Collider2D nearby = Physics2D.OverlapCircle(point, minDistance, spikeMask);
                if (nearby != null) isValid = false;

                Gizmos.color = isValid ? Color.green : Color.red;
                Gizmos.DrawWireSphere(point, cellSize * .3f);
            }
        }
    }
}
