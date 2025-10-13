using System.Collections;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    [SerializeField] private float timeBetweenSummons = 5f;
    [SerializeField] private Enemy[] enemyToSummon;
    [SerializeField] private bool summonDuringMove = false;
    
    private PatrolController patrolController;
    private Coroutine summonRoutine;

    private void Start()
    {
        patrolController = GetComponent<PatrolController>();
        summonRoutine = StartCoroutine(SummonLoop());
    }

    private IEnumerator SummonLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenSummons);
            if (summonDuringMove && patrolController.IsMoving ||
                !summonDuringMove && !patrolController.IsMoving)
            {
                SummonRandomEnemy();
            }
        }
    }

    private void SummonRandomEnemy()
    {
        Instantiate(enemyToSummon[GetRandomIndex(enemyToSummon.Length)], transform.position, transform.rotation);
    }

    private int GetRandomIndex(int length) => Random.Range(0, length);

    public void SetSummonDuringMovement(bool value)
    {
        summonDuringMove = value;
    }

    private void OnDisable()
    {
        if (summonRoutine != null) StopCoroutine(summonRoutine);
    }
}
