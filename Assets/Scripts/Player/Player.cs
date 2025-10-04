using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Health health;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath.AddListener(OnPlayerDeath);
    }

    private void OnDisable()
    {
        health.OnDeath.RemoveListener(OnPlayerDeath);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable pickable = collision.GetComponent<IPickable>();
        if(pickable != null)
        {
            pickable.OnPickUp(gameObject);
        }
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Death!");
        gameObject.SetActive(false);
    }
}
