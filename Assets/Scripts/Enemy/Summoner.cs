using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    [SerializeField] private float minX = -18f;
    [SerializeField] private float maxX = 18f;
    [SerializeField] private float minY= -12f;
    [SerializeField] private float maxY= 12f;
    [SerializeField] private float timeBetweenSummons = 5f;
    [SerializeField] private Enemy enemyToSummon;

    private Vector2 targetPosition;
    private float summonTime;
    private Animator anim;
    private MeleeAttack meleeAttack;

    protected override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    private void Update()
    {
        if(playerTransform != null)
        {
            if(Vector2.Distance(transform.position, targetPosition) > .25f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);

                if(Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                    Summon();
                }
            }

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

    public void Summon()
    {
        if(playerTransform != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }
}
