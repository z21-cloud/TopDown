using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float timeBetweenAttacks = 2f;
    [SerializeField] protected float attackSpeed = 2f;
    [SerializeField] protected float stopDistance = 2f;
    
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;

    [SerializeField] private DropTable dropTable;
    protected Transform playerTransform;
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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = new Timer(timeBetweenAttacks);

    }

    protected virtual void Update()
    {
        if (playerTransform != null)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
            {
                MoveToPlayer();
            }
            else
                TryAttack();
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Speed * Time.deltaTime);
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
        Destroy(gameObject);
        //add listener to set active false ;  object pooling
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
