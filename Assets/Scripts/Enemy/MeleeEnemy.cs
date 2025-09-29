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

    protected override void Attack()
    {
        StartCoroutine(meleeAttack.AttackCoroutine(attackSpeed, playerTransform, Damage));
    }
}
