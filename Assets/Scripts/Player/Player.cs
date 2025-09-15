using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float speed = 5f;

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

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log(Health);
        if (Health <= 0)
            Death(); //Listeners?
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
