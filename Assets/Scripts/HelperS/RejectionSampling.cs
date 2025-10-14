using UnityEngine;

public static class RejectionSampling
{
    public static Vector3 GetValidSpawnPoint(Collider2D bossCollider, float radius, float minDistance, int attempCount, LayerMask spikeMask)
    {
        Vector3 bossPos = bossCollider.transform.position;
        for (int i = 0; i < attempCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * radius;
            Vector3 point = bossPos + new Vector3(randomOffset.x, randomOffset.y, 0);

            if (bossCollider.OverlapPoint(point)) continue;
            
            Collider2D nearby = Physics2D.OverlapCircle(point, minDistance, spikeMask);
            if (nearby != null) continue;

            return point;
        }
        return bossPos;
    }
}
