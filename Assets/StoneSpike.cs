using UnityEngine;

public class StoneSpike : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            return;
        }
    }

    private void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
