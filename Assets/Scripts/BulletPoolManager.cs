using UnityEngine;
using System.Collections.Generic;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletsCount= 10;

    private List<GameObject> pool;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < bulletsCount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in pool)
        {
            if(!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }
}
