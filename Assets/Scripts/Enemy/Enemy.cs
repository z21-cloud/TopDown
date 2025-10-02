using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float timeBetweenAttacks = 2f;
    [SerializeField] protected float attackSpeed = 2f;
    [SerializeField] protected float stopDistance = 2f;
    [SerializeField][Range(0f, 1f)] protected float dropChance = .2f;
    [SerializeField] private List<GameObject> weaponsPrefabs;
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

    private void GenerateDropChance()
    {
        float roll = Random.value;
        if(roll <= dropChance && weaponsPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, weaponsPrefabs.Count);
            Instantiate(weaponsPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
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

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log(Health);
        if (Health <= 0)
            Death();
    }

    private void Death()
    {
        GenerateDropChance();
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
