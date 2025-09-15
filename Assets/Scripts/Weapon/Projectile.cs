using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private GameObject hitEffects;

    private Vector2 direction;
    private bool isMoving = false;

    public void Initialize(Vector2 moveDirection, float projectileSpeed, float projectileLifeTime, int projectileDamage, GameObject effects)
    {
        direction = moveDirection.normalized;
        speed = projectileSpeed;
        this.projectileLifeTime = projectileLifeTime;
        damage = projectileDamage;
        hitEffects = effects;

        StartMoving();
    }

    private void StartMoving()
    {
        isMoving = true;
        StartCoroutine(MoveCoroutine());
        StartCoroutine(LifetimeCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while(isMoving && gameObject != null)
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(projectileLifeTime);
        if (gameObject != null) DestroyProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            DestroyProjectile();
            return;
        }

        if(other.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
    }

    public void DestroyProjectile()
    {
        isMoving = false;

        if (hitEffects != null)
            Instantiate(hitEffects,
                        transform.position,
                        Quaternion.identity);
        Destroy(gameObject);
    }
}
