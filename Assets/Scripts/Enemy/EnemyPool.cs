using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class EnemyPrefabEntry
{
    public EnemyType type;
    public GameObject prefab;
}

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<EnemyPrefabEntry> enemiesPrefabs;
    private Dictionary<EnemyType, List<GameObject>> pooledObjects = new Dictionary<EnemyType, List<GameObject>>();
    private int poolSizePerType = 4;

    public static EnemyPool Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        foreach(var enemy in enemiesPrefabs)
        {
            List<GameObject> temp = new List<GameObject>();
            for(int i = 0; i < poolSizePerType; i++)
            {
                GameObject obj = Instantiate(enemy.prefab);
                obj.SetActive(false);
                temp.Add(obj);
            }
            pooledObjects.Add(enemy.type, temp);
        }
    }

    public GameObject GetPooledObject(EnemyType type)
    {
        if (!pooledObjects.ContainsKey(type)) return null;

        List<GameObject> temp = pooledObjects[type];

        for (int i = 0; i < temp.Count; i++)
        {
            if (!temp[i].activeInHierarchy)
            {
                return temp[i];
            }
        }
        /*GameObject prefab = enemiesPrefabs.Find(e => e.type == name);
        if(prefab != null)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.SetActive(false);
            temp.Add(newObject);
            return newObject;
        }*/

        return null;
    }
}
