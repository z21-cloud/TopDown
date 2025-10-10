using UnityEngine;

public class Summon : MonoBehaviour
{
    [SerializeField] private float timeBetweenSummons = 5f;
    [SerializeField] private Enemy[] enemyToSummon;
    private float summonTime;
    private PatrolController patrolController;  

    private void Start()
    {
        patrolController = GetComponent<PatrolController>();
        if (patrolController != null)
        {
            patrolController.OnSummonStateChanged += HandleSummonState;
        }
    }

    public void HandleSummonState(bool canSummon)
    {
        if (Time.time >= summonTime && canSummon)
        {
            summonTime = Time.time + timeBetweenSummons;
            SummonRandomEnemy();
        }
    }

    private void SummonRandomEnemy()
    {
        Instantiate(enemyToSummon[GetRandomIndex(enemyToSummon.Length)], transform.position, transform.rotation);
    }

    private int GetRandomIndex(int length) => Random.Range(0, length);

    private void OnDestroy()
    {
        if (patrolController != null)
            patrolController.OnSummonStateChanged -= HandleSummonState;
    }
}
