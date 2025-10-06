using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickable
{
    [SerializeField] private int healAmount = 1;

    public void OnPickUp(GameObject picker)
    {
        gameObject.SetActive(false);
    }

    public PickableType GetPickableType() => PickableType.Hearts;

    public void ApplyHeal(Health health)
    {
        if(health != null)
            health.Heal(healAmount);
    }
}
