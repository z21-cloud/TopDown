using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float attackSpeed = 2f;

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
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        playerTransform.GetComponent<Player>().TakeDamage(Damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = playerTransform.position;

        float percent = 0;
        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
