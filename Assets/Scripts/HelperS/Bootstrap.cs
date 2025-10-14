using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player player;
    private void Awake()
    {
        ServiceLocator.Register<Player>(player);
    }
}
