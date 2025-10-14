using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float timeBetweenAttacks = 2f;
    [SerializeField] protected float attackSpeed = 2f;
    [SerializeField] protected float stopDistance = 2f;
    [SerializeField] protected GameObject deathEffects;
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;

    [SerializeField] private DropTable dropTable;
    protected Transform playerTransform;
    protected IMovementStrategy movementStrategy;
    protected Timer attackTimer;
    protected Health health;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        health.OnDeath.RemoveListener(Death);
    }

    protected virtual void Start()
    {
        playerTransform = ServiceLocator.Get<Player>().transform;
        attackTimer = new Timer(timeBetweenAttacks);
        movementStrategy = new ChasePlayerMovement();
    }

    protected virtual void Update()
    {
        if (playerTransform != null)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
            {
                movementStrategy.Move(transform, playerTransform, Speed);
            }
            else
                TryAttack();
        }
    }

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


    private void Death()
    {
        dropTable?.GenerateDropChance(transform.position);
        CreateDeathEffect();
        Destroy(gameObject);
        //add listener to set active false ;  object pooling
    }

    private void CreateDeathEffect()
    {
        GameObject effects = Instantiate(deathEffects, transform.position, Quaternion.identity);
        Destroy(effects, 1f);
    }

    private void OnDrawGizmos()
    {
        if (playerTransform == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, playerTransform.position);

        /*float distance = Vector2.Distance(transform.position, playerTransform.position);
        Handles.Label(
            (playerTransform.position + transform.position) / 2, 
            distance.ToString("F1")
            ); */
    }
}
