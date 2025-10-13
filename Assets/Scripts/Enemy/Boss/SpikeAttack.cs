using UnityEngine;
using System.Collections.Generic;

public class SpikeAttack : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private int minCount = 5;
    [SerializeField] private int maxCount = 10;
    [SerializeField] private int attemptsCount = 100;
    [SerializeField] private LayerMask spikeMask;
    [SerializeField] private GameObject spikePrefab;

    private int spikeCount;
    private Collider2D bossCollider;
    private void Awake()
    {
        bossCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    public void StartAttack()
    {
        spikeCount = Random.Range(minCount, maxCount);
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < spikeCount; i++)
        {
            Vector3 point = GridSampling.GetValidSpawnPoint(bossCollider, radius, minDistance, spikeMask);
            points.Add(point);
        }

        foreach (Vector3 point in points)
        {
            Instantiate(spikePrefab, point, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        if (bossCollider == null)
            bossCollider = GetComponent<Collider2D>();

        if(bossCollider != null)
            GridSampling.DrawGrid(bossCollider.transform.position, radius, minDistance, bossCollider, minDistance, spikeMask);
    }
}
