using System;
using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks = 2f;
    private SpikeAttack spikeAttack;
    private PatrolController patrolController;
    private SummonController summonController;
    private Coroutine attackCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spikeAttack = GetComponent<SpikeAttack>();
        patrolController = GetComponent<PatrolController>();
        summonController = GetComponent<SummonController>();

        if(patrolController != null)
        {
            patrolController.OnMoveStateChanged += HandleMoveState;
            patrolController.StartPatrol();
        }

        if(summonController != null)
        {
            summonController.SetSummonDuringMovement(true);
        }
    }

    private void HandleMoveState(bool isMoving)
    {
        if (!isMoving)
        {
            if(attackCoroutine == null)
                attackCoroutine = StartCoroutine(AttackCoroutine());
        }
        else
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        spikeAttack.StartAttack();
        yield return new WaitForSeconds(timeBetweenAttacks);
    }

    private void OnDestroy()
    {
        if(patrolController != null)
        {
            patrolController.OnMoveStateChanged -= HandleMoveState;
        }
    }
}
