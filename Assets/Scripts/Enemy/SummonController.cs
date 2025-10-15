using System.Collections;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    [SerializeField] private float timeBetweenSummons = 5f;
    [SerializeField] private Enemy[] enemyToSummon;
    [SerializeField] private bool summonDuringMove = false;
    
    private PatrolController patrolController;
    private Coroutine summonRoutine;
    private bool isMoving;

    private void Start()
    {
        patrolController = GetComponent<PatrolController>();
        if (patrolController != null)
            patrolController.OnMoveStateChanged += MoveState;
        summonRoutine = StartCoroutine(SummonLoop());
    }

    private void MoveState(bool value)
    {
        isMoving = value;
    }

    private IEnumerator SummonLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenSummons);
            if (summonDuringMove && isMoving ||
                !summonDuringMove && !isMoving)
            {
                SummonRandomEnemy();
            }
        }
    }

    private void SummonRandomEnemy()
    {
        Instantiate(EnemyPool.Instance.GetPooledObject(enemyToSummon[GetRandomIndex(enemyToSummon.Length)].type), transform.position, transform.rotation);
    }

    private int GetRandomIndex(int length) => Random.Range(0, length);

    public void SetSummonDuringMovement(bool value)
    {
        summonDuringMove = value;
    }

    private void OnDisable()
    {
        if (summonRoutine != null) StopCoroutine(summonRoutine);
        if (patrolController != null)
            patrolController.OnMoveStateChanged -= MoveState;
    }
}
