using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    private Animator anim;
    private MeleeAttack meleeAttack;
    private PatrolController patrolController;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        meleeAttack = GetComponent<MeleeAttack>();
        patrolController = GetComponent<PatrolController>();

        if (patrolController != null)
        {
            patrolController.OnMoveStateChanged += HandleMoveStateChanged;
            patrolController.OnSummonStateChanged += HandleSummonAnimStateChanged;
            patrolController.StartPatrol();
        }
    }

    private void HandleSummonAnimStateChanged(bool isSummoning)
    {
        if(isSummoning) anim.SetTrigger("summon");
    }

    private void HandleMoveStateChanged(bool isMoving)
    {
        anim.SetBool("isRunning", isMoving);
    }

    private void OnDestroy()
    {
        if (patrolController != null)
        {
            patrolController.OnSummonStateChanged -= HandleSummonAnimStateChanged;
            patrolController.OnMoveStateChanged -= HandleMoveStateChanged;
        }
    }

    protected override void Update()
    {
        if(playerTransform != null)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < stopDistance)
            {
                TryAttack();
            }
        }
    }

    protected override void Attack()
    {
        StartCoroutine(meleeAttack.AttackCoroutine(attackSpeed, playerTransform, Damage));
    }
}
