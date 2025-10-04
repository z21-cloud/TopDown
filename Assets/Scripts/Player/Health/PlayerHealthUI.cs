using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private HeartPool heartPool;
    [SerializeField] private Health health;

    private void OnEnable()
    {
        health.OnHealthChanged.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        health.OnHealthChanged.RemoveListener(UpdateUI);
    }

    private void UpdateUI(int current, int max)
    {
        heartPool.UpdateHearts(current, max);
    }
}
