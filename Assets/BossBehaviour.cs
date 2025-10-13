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
    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
        spikeAttack = GetComponent<SpikeAttack>();
        patrolController = GetComponent<PatrolController>();
        summonController = GetComponent<SummonController>();

        if(patrolController != null)
        {
            patrolController.OnMoveStateChanged += HandleMoveState;
        }

        if(summonController != null)
        {
            summonController.SetSummonDuringMovement(true);
        }

        if(health != null)
        {
            health.OnDeath.AddListener(Death);
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
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            spikeAttack.StartAttack();
        }
    }

    public void AnimationEnding()
    {
        if (patrolController != null)
        {
            patrolController.StartPatrol();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if(patrolController != null)
        {
            patrolController.OnMoveStateChanged -= HandleMoveState;
        }
    }

    private void OnDisable()
    {
        health.OnDeath.RemoveListener(Death);
    }
}
