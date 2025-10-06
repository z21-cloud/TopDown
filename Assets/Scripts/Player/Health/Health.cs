using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int healthBreak = 5;
    private int currentHealth;

    public UnityEvent<int, int> OnHealthChanged;
    public UnityEvent OnDeath;

    public int Current => currentHealth;
    public int Max => maxHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if(currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth >= maxHealth && currentHealth < healthBreak) AddHeart();

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void AddHeart()
    {
        maxHealth++;
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
