using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public IEnumerator AttackCoroutine(float attackSpeed, Transform playerTransform, float damage)
    {
        playerTransform.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = playerTransform.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
