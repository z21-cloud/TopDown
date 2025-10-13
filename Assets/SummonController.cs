using UnityEngine;

public class SummonController : MonoBehaviour
{
    [SerializeField] private float timeBetweenSummons = 5f;
    [SerializeField] private Enemy[] enemyToSummon;
    [SerializeField] private bool summonDuringMove = false;
    
    private float summonTime;
    private PatrolController patrolController;  

    private void Start()
    {
        patrolController = GetComponent<PatrolController>();
        if (patrolController != null)
        {
            patrolController.OnMoveStateChanged += HandleMoveState;
        }
    }

    private void HandleMoveState(bool isMoving)
    {
        if (Time.time >= summonTime)
        {
            if(summonDuringMove && isMoving || 
                !summonDuringMove && !isMoving)
            {
                summonTime = Time.time + timeBetweenSummons;
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

    private void OnDestroy()
    {
        if (patrolController != null)
            patrolController.OnMoveStateChanged -= HandleMoveState;
    }
}
