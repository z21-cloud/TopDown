using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedEnemy : Enemy
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private GameObject projectileEffects;
    private Animator anim;
    private Projectile projectile;
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (playerTransform != null)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
            {
                MoveToPlayer();
            }
            TryAttack();
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Speed * Time.deltaTime);
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

        GameObject newProjectile = Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
        Projectile projectileDamage = newProjectile.GetComponent<Projectile>();
        if (projectileDamage != null)
        {
            projectileDamage.Initialize(direction, bulletSpeed, bulletLifetime, bulletDamage, projectileEffects);
        }
    }
}
