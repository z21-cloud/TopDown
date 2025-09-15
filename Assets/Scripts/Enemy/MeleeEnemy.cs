using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private MeleeAttack meleeAttack;

    protected override void Start()
    {
        base.Start();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    private void Update()
    {
        if(playerTransform != null)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Speed * Time.deltaTime);
        }
        else
            TryAttack();
    }

    protected override void Attack()
    {
        StartCoroutine(meleeAttack.AttackCoroutine(attackSpeed, playerTransform, Damage));
    }
}
