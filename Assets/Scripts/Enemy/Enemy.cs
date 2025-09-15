using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float timeBetweenAttacks = 2f;
    [SerializeField] protected float attackSpeed = 2f;
    [SerializeField] protected float stopDistance = 2f;
    [SerializeField] private float health = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;

    protected Transform playerTransform;
    protected Timer attackTimer;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = new Timer(timeBetweenAttacks);
    }

    /*private void Update()
    {
        TryAttack();
    }*/

    protected void TryAttack()
    {
        attackTimer.Update(Time.deltaTime);

        if (attackTimer.isReady)
        {
            Attack();
            attackTimer.Reset();
        }
    }

    protected abstract void Attack();

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log(Health);
        if (Health <= 0)
            Death();
    }

    private void Death()
    {
        Destroy(gameObject);
        //add listener to set active false ;  object pooling
    }
}
