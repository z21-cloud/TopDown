using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject enemyBullet;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(playerTransform != null)
        {
            if(Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Speed * Time.deltaTime);
            }
            else
                TryAttack();
        }
    }

    protected override void Attack()
    {
        Debug.Log("attacking");
        anim.SetTrigger("attack");
    }

    public void RangedAttack()
    {
        Vector2 direction = playerTransform.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.normalized.x, direction.normalized.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);
        shotPoint.transform.rotation = targetRotation;

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
    }
}
