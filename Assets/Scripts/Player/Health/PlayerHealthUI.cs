using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private HeartPool heartPool;
    [SerializeField] private Health health;

    private void Awake()
    {
        if (health == null)
            health = ServiceLocator.Get<Player>()?.GetComponent<Health>();

        if (heartPool == null)
            heartPool = GetComponentInChildren<HeartPool>();
    }

    private void OnEnable()
    {
        if(health != null)
        {
            health.OnHealthChanged.AddListener(UpdateUI);
            UpdateUI(health.Current, health.Max);
        }
    }

    private void OnDisable()
    {
        if(health != null) 
            health.OnHealthChanged.RemoveListener(UpdateUI);
    }

    private void UpdateUI(int current, int max)
    {
        if(heartPool != null) 
            heartPool.UpdateHearts(current, max);
    }
}
